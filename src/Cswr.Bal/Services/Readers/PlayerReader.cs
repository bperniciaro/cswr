// <copyright file="PlayerReader.cs" company="Fornits Web Solutions">
// Copyright (c) Fornits Web Solutions. All rights reserved.
// </copyright>

using System.Data.Common;
using AutoMapper;
using Cswr.Bal.Domain;
using Cswr.Bal.Lib;
using Cswr.Bal.Services.Interfaces.Readers;
using Cswr.Dal.Context;
using Cswr.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Cswr.Bal.Services.Readers;

/// <summary>
/// Service responsible for reading player information.
/// </summary>
public class PlayerReader : IPlayerReader
{
    private readonly CswrDbContext cswrDbContext;
    private readonly ILogger<CheatSheetReader> logger;
    private readonly IMapper mapper;

    private IMemoryCache cache;


    /// <summary>
    /// Initializes a new instance of the <see cref="PlayeReader"/> class.
    /// </summary>
    /// <param name="cswrDbContext">Service responsible for communicating with the database.</param>
    /// <param name="logger">Service reposponsible for logging information.</param>
    /// <param name="mapper">Service responsible for mapping objects.</param>
    /// <param name="cache">Service responsible for caching objects.</param>
    public PlayerReader(CswrDbContext cswrDbContext, ILogger<CheatSheetReader> logger, IMapper mapper, IMemoryCache cache)
    {
        this.cswrDbContext = cswrDbContext;
        this.logger = logger;
        this.mapper = mapper;
        this.cache = cache;
    }

    /// <inheritdoc/>
    public async Task<Result<IEnumerable<Player>>> GetPlayersNotInSheet(int cheatSheetId)
    {
        List<Player> playersNotOnSheet = new List<Player>();

        try
        {
            playersNotOnSheet = this.cswrDbContext.SheetsPlayers
                .Join(
                    this.cswrDbContext.SheetsCheatSheetPositions,
                    p => p.PositionCode,
                    csp => csp.PositionCode,
                    (p, csp) => new { p, csp })
                .Join(
                    this.cswrDbContext.SheetsCheatSheets,
                    pc => pc.csp.CheatSheetId,
                    cs => cs.CheatSheetId,
                    (pc, cs) => new { pc.p, pc.csp, cs })
                .Where(x => x.csp.CheatSheetId == cheatSheetId
                            && x.p.FirstYear.Year <= Convert.ToInt32(x.cs.SeasonCode)
                            && x.p.Retired == false
                            && !this.cswrDbContext.SheetsCheatSheetItems.Any(item => item.PlayerId == x.p.PlayerId && item.CheatSheetId == cheatSheetId))
                .OrderBy(x => x.p.LastName)
                .ThenBy(x => x.p.FirstName)
                .Select(x => new Player
                {
                    PlayerId = x.p.PlayerId,
                    SportCode = x.p.SportCode,
                    PositionCode = x.p.PositionCode,
                    FirstName = x.p.FirstName,
                    LastName = x.p.LastName,
                    MiddleName = x.p.MiddleName,
                    TeamCode = x.p.TeamCode,
                    Number = x.p.Number,
                    FirstYear = x.p.FirstYear,
                    BirthDate = x.p.BirthDate,
                    TwitterUsername = x.p.TwitterUsername,
                    StatMapId = x.p.StatMapId,
                    Retired = x.p.Retired,
                })
            .ToList();
        }
        catch (DbException ex)
        {
            return Result.Fail<IEnumerable<Player>>("DB Exception when calling GetPlayersNotInSheet: " + ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<Player>>("General Exception when calling GetPlayersNotInSheet: " + ex.Message);
        }

        return Result.Ok(this.mapper.Map<IEnumerable<Player>>(playersNotOnSheet));
    }
}
