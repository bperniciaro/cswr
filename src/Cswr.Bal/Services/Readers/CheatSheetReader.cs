// <copyright file="CheatSheetReader.cs" company="Fornits Web Solutions">
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
public class CheatSheetReader : ICheatSheetReader
{
    private readonly CswrDbContext cswrDbContext;
    private readonly ILogger<CheatSheetReader> logger;
    private readonly IMapper mapper;

    private IMemoryCache cache;


    /// <summary>
    /// Initializes a new instance of the <see cref="CheatSheetReader"/> class.
    /// </summary>
    /// <param name="cswrDbContext">Service responsible for communicating with the database.</param>
    /// <param name="logger">Service reposponsible for logging information.</param>
    /// <param name="mapper">Service responsible for mapping objects.</param>
    /// <param name="cache">Service responsible for caching objects.</param>
    public CheatSheetReader(CswrDbContext cswrDbContext, ILogger<CheatSheetReader> logger, IMapper mapper, IMemoryCache cache)
    {
        this.cswrDbContext = cswrDbContext;
        this.logger = logger;
        this.mapper = mapper;
        this.cache = cache;
    }

    /// <inheritdoc/>
    public async Task<Result<CheatSheet>> GetCheatSheet(int cheatSheetID)
    {
        SheetsCheatSheet targetSheet;

        try
        {
            targetSheet = await this.cswrDbContext.SheetsCheatSheets.Include(x => x.SheetsCheatSheetItems).SingleAsync(x => x.CheatSheetId == cheatSheetID);

            if (targetSheet == null)
            {
                return Result.Fail<CheatSheet>("Sheet not found");
            }

            return Result.Ok(this.mapper.Map<CheatSheet>(targetSheet));
        }
        catch (DbException ex)
        {
            return Result.Fail<CheatSheet>("Database Exception when calling GetCheatSheet: " + ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Fail<CheatSheet>("General Exception when calling GetCheatSheet: " + ex.Message);
        }
    }

    /// <inheritdoc/>
    //public async Task<Result<IEnumerable<CheatSheet>>> GetCheatSheets(string userName)
    //{
    //    IEnumerable<SheetsCheatSheet> userSheets;

    //    try
    //    {
    //        userSheets = await this.cswrDbContext.SheetsCheatSheets.Where(x => x.Username == userName).ToListAsync();
    //    }
    //    catch (DbException ex)
    //    {
    //        return Result.Fail<IEnumerable<CheatSheet>>("Database Exception when calling GetCheatSheets: " + ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        return Result.Fail<IEnumerable<CheatSheet>>("General Exception when calling GetCheatSheets: " + ex.Message);
    //    }

    //    return Result.Ok(this.mapper.Map<IEnumerable<CheatSheet>>(userSheets));
    //}


    /// <inheritdoc/>
    public async Task<Result<IEnumerable<CheatSheet>>> GetCheatSheets(string userName)
    {
        // Check if the record is in the cache
        if (!this.cache.TryGetValue(userName, out IEnumerable<SheetsCheatSheet> userSheets))
        {
            try
            {
                userSheets = await this.cswrDbContext.SheetsCheatSheets.Where(x => x.Username == userName).ToListAsync();
            }
            catch (DbException ex)
            {
                return Result.Fail<IEnumerable<CheatSheet>>("DB Exception when calling GetCheatSheets: " + ex.Message);
            }
            catch (Exception ex)
            {
                return Result.Fail<IEnumerable<CheatSheet>>("General Exception when calling GetCheatSheets: " + ex.Message);
            }

            // Set cache options
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(2)); // Cache for 2 hours

            // Save the result in the cache
            this.cache.Set(userName, userSheets, cacheEntryOptions);
        }

        return Result.Ok(this.mapper.Map<IEnumerable<CheatSheet>>(userSheets));
    }
}
