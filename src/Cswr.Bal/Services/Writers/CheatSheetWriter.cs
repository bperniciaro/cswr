// <copyright file="CheatSheetWriter.cs" company="Fornits Web Solutions">
// Copyright (c) Fornits Web Solutions. All rights reserved.
// </copyright>

using AutoMapper;
using Cswr.Bal.Domain;
using Cswr.Bal.Lib;
using Cswr.Bal.Services.Interfaces.Writers;
using Cswr.Dal.Context;
using Cswr.Dal.Models;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Cswr.Bal.Services.Writers;

/// <summary>
/// Service responsible for writing cheat sheet information.
/// </summary>
public class CheatSheetWriter : ICheatSheetWriter
{
    private readonly CswrDbContext cswrDbContext;
    private readonly ILogger<CheatSheetWriter> logger;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CheatSheetWriter"/> class.
    /// </summary>
    /// <param name="cswrDbContext">Service responsible for communicating with the database.</param>
    /// <param name="logger">Service reposponsible for logging information.</param>
    /// <param name="mapper">Service responsible for mapping objects.</param>
    public CheatSheetWriter(CswrDbContext cswrDbContext, ILogger<CheatSheetWriter> logger, IMapper mapper)
    {
        this.cswrDbContext = cswrDbContext;
        this.logger = logger;
        this.mapper = mapper;
    }

    public Task<Result<bool>> AddItems(CheatSheet cheatSheet, List<int> playersToAdd)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<Result<bool>> UpdateCheatSheet(CheatSheet targetSheet)
    {
        // Make sure the sheet passed in is not null
        if (targetSheet == null)
        {
            return Result.Fail<bool>("You're trying to update a sheet that is null");
        }

        int recordUpdateCount = 0;

        try
        {

            var entitySheet = this.mapper.Map<SheetsCheatSheet>(targetSheet);

            // This is the easiest way, but do you need to be tracking?  You must have all 
            // properties of the sheet populated
            //this.cswrDbContext.Update(entitySheet);
            //recordUpdateCount = await this.cswrDbContext.SaveChangesAsync();

            // If we weren't tracking, we could use this since it sets the state to Modified manually
            //this.cswrDbContext.Entry(entitySheet).State = EntityState.Modified;
            //var result = await this.cswrDbContext.SaveChangesAsync();

            // Best when only updating a small sample of properties
            var dbSheet = this.cswrDbContext.SheetsCheatSheets.Find(targetSheet.CheatSheetId);
            if (dbSheet == null)
            {
                return Result.Fail<bool>("The target user was not found.");
            }
            dbSheet.SheetName = targetSheet.SheetName;
            var result = this.cswrDbContext.SaveChangesAsync();
        }
        catch (DbException ex)
        {
            return Result.Fail<bool>("Database Exception when calling UpdateCheatSheet: " + ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Fail<bool>("General Exception when calling GetCheatSheets: " + ex.Message);
        }

        // Ensure only record was updated, or there was a problem.
        if (recordUpdateCount != 1)
        {
            return Result.Fail<bool>("Problem updating target sheet");
        }

        return Result.Ok<bool>(true);
    }


}
