// <copyright file="CheatSheetItemReader.cs" company="Fornits Web Solutions">
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
/// Service responsible for reading cheat sheet information.
/// </summary>
public class CheatSheetItemReader : ICheatSheetItemReader
{
    private readonly CswrDbContext cswrDbContext;
    private readonly ILogger<CheatSheetItemReader> logger;
    private readonly IMapper mapper;

    private IMemoryCache cache;


    /// <summary>
    /// Initializes a new instance of the <see cref="CheatSheetItemReader"/> class.
    /// </summary>
    /// <param name="cswrDbContext">Service responsible for communicating with the database.</param>
    /// <param name="logger">Service reposponsible for logging information.</param>
    /// <param name="mapper">Service responsible for mapping objects.</param>
    /// <param name="cache">Service responsible for caching objects.</param>
    public CheatSheetItemReader(CswrDbContext cswrDbContext, ILogger<CheatSheetItemReader> logger, IMapper mapper, IMemoryCache cache)
    {
        this.cswrDbContext = cswrDbContext;
        this.logger = logger;
        this.mapper = mapper;
        this.cache = cache;
    }

    /// <inheritdoc/>
    public async Task<Result<IEnumerable<CheatSheetItem>>> GetCheatSheetItems(int cheatSheetId)
    {
        IEnumerable<SheetsCheatSheetItem> targetSheetItems;

        try
        {
            // .Include(x => x.Player)
            targetSheetItems = await this.cswrDbContext.SheetsCheatSheetItems.Where(x => x.CheatSheetId == cheatSheetId).Include(x => x.Player).OrderBy(x => x.Seqno).ToListAsync();
        }
        catch (DbException ex)
        {
            return Result.Fail<IEnumerable<CheatSheetItem>>("Database Exception when calling GetCheatSheetItems: " + ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<CheatSheetItem>>("General Exception when calling GetCheatSheetItems: " + ex.Message);
        }

        if (!targetSheetItems.Any())
        {
            return Result.Fail<IEnumerable<CheatSheetItem>>("No cheat sheet items not found");
        }

        return Result.Ok(this.mapper.Map<IEnumerable<CheatSheetItem>>(targetSheetItems));
    }
}
