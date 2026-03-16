// <copyright file="CheatSheetWriterTests.cs" company="Fornits Web Solutions">
// Copyright (c) Fornits Web Solutions. All rights reserved.
// </copyright>

using AutoMapper;
using Cswr.Bal.Domain;
using Cswr.Bal.Lib.Mappers;
using Cswr.Bal.Services.Writers;
using Cswr.Dal.Context;
using Cswr.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace Cswr.Bal.Tests.Services.Writers;

/// <summary>
/// Tests for <see cref="CheatSheetWriter"/>.
/// </summary>
[TestClass]
public class CheatSheetWriterTests
{
    private CswrDbContext _context = null!;
    private CheatSheetWriter _writer = null!;

    /// <summary>
    /// Creates a fresh in-memory database and writer instance before each test.
    /// </summary>
    [TestInitialize]
    public void Initialize()
    {
        var options = new DbContextOptionsBuilder<CswrDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new CswrDbContext(options);

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAutoMapper(cfg => cfg.AddProfile<BalMappingProfile>());
        var mapper = services.BuildServiceProvider().GetRequiredService<IMapper>();

        _writer = new CheatSheetWriter(_context, NullLogger<CheatSheetWriter>.Instance, mapper);
    }

    /// <summary>
    /// Disposes the database context after each test.
    /// </summary>
    [TestCleanup]
    public void Cleanup() => _context.Dispose();

    /// <summary>
    /// Passing a null sheet should return a failure result without touching the database.
    /// </summary>
    [TestMethod]
    public async Task UpdateCheatSheet_NullSheet_ReturnsFailure()
    {
        var result = await _writer.UpdateCheatSheet(null!);

        Assert.IsTrue(result.Failure);
        Assert.IsFalse(string.IsNullOrEmpty(result.Message));
    }

    /// <summary>
    /// Passing a sheet ID that does not exist in the database should return a failure result.
    /// </summary>
    [TestMethod]
    public async Task UpdateCheatSheet_SheetNotFound_ReturnsFailure()
    {
        var sheet = new CheatSheet { CheatSheetId = 9999, SheetName = "Ghost Sheet" };

        var result = await _writer.UpdateCheatSheet(sheet);

        Assert.IsTrue(result.Failure);
    }

    /// <summary>
    /// Passing a valid sheet with an existing ID should persist the name change and return success.
    /// </summary>
    [TestMethod]
    public async Task UpdateCheatSheet_ValidSheet_ReturnsSuccess()
    {
        _context.SheetsCheatSheets.Add(new SheetsCheatSheet
        {
            CheatSheetId = 1,
            SheetName = "Original Name",
            Username = "test@example.com",
            SeasonCode = "2025",
            StatsSeasonCode = "2025",
            SportCode = "FOO",
        });
        await _context.SaveChangesAsync();

        var sheet = new CheatSheet { CheatSheetId = 1, SheetName = "Updated Name" };

        var result = await _writer.UpdateCheatSheet(sheet);

        Assert.IsTrue(result.Success);
        Assert.AreEqual("Updated Name", _context.SheetsCheatSheets.Find(1)!.SheetName);
    }

    /// <summary>
    /// Updating PprLeague and AuctionDraft should persist both fields.
    /// </summary>
    [TestMethod]
    public async Task UpdateCheatSheet_PprAndAuctionFields_PersistedCorrectly()
    {
        _context.SheetsCheatSheets.Add(new SheetsCheatSheet
        {
            CheatSheetId = 2,
            SheetName = "My Sheet",
            Username = "test@example.com",
            SeasonCode = "2025",
            StatsSeasonCode = "2025",
            SportCode = "FOO",
            Pprleague = false,
            AuctionDraft = false,
        });
        await _context.SaveChangesAsync();

        var sheet = new CheatSheet { CheatSheetId = 2, SheetName = "My Sheet", PprLeague = true, AuctionDraft = true };

        var result = await _writer.UpdateCheatSheet(sheet);

        Assert.IsTrue(result.Success);
        var saved = _context.SheetsCheatSheets.Find(2)!;
        Assert.IsTrue(saved.Pprleague);
        Assert.IsTrue(saved.AuctionDraft);
    }
}
