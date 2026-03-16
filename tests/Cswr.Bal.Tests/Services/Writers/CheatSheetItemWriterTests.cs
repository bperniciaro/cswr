// <copyright file="CheatSheetItemWriterTests.cs" company="Fornits Web Solutions">
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
/// Tests for <see cref="CheatSheetWriter"/> AddItems and RemoveItems.
/// </summary>
[TestClass]
public class CheatSheetItemWriterTests
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

    // --- AddItems ---

    /// <summary>
    /// Passing an empty player list should succeed without inserting any rows.
    /// </summary>
    [TestMethod]
    public async Task AddItems_EmptyPlayerList_ReturnsSuccessWithNoInserts()
    {
        var sheet = new CheatSheet { CheatSheetId = 1 };

        var result = await _writer.AddItems(sheet, new List<int>());

        Assert.IsTrue(result.Success);
        Assert.AreEqual(0, _context.SheetsCheatSheetItems.Count());
    }

    /// <summary>
    /// Adding players to an empty sheet should insert items with Seqno starting at 1.
    /// </summary>
    [TestMethod]
    public async Task AddItems_EmptySheet_InsertsItemsStartingAtSeqnoOne()
    {
        var sheet = new CheatSheet { CheatSheetId = 1 };

        var result = await _writer.AddItems(sheet, new List<int> { 101, 102, 103 });

        Assert.IsTrue(result.Success);
        var items = _context.SheetsCheatSheetItems.OrderBy(x => x.Seqno).ToList();
        Assert.AreEqual(3, items.Count);
        Assert.AreEqual(1, items[0].Seqno);
        Assert.AreEqual(101, items[0].PlayerId);
        Assert.AreEqual(2, items[1].Seqno);
        Assert.AreEqual(3, items[2].Seqno);
    }

    /// <summary>
    /// Adding players to a sheet that already has items should append after the current max Seqno.
    /// </summary>
    [TestMethod]
    public async Task AddItems_ExistingItems_AppendsAfterMaxSeqno()
    {
        _context.SheetsCheatSheetItems.AddRange(
            new SheetsCheatSheetItem { CheatSheetId = 1, PlayerId = 10, Seqno = 1, Note = string.Empty },
            new SheetsCheatSheetItem { CheatSheetId = 1, PlayerId = 20, Seqno = 2, Note = string.Empty });
        await _context.SaveChangesAsync();

        var sheet = new CheatSheet { CheatSheetId = 1 };

        var result = await _writer.AddItems(sheet, new List<int> { 30, 40 });

        Assert.IsTrue(result.Success);
        var newItems = _context.SheetsCheatSheetItems
            .Where(x => x.PlayerId == 30 || x.PlayerId == 40)
            .OrderBy(x => x.Seqno)
            .ToList();
        Assert.AreEqual(2, newItems.Count);
        Assert.AreEqual(3, newItems[0].Seqno);
        Assert.AreEqual(4, newItems[1].Seqno);
    }

    // --- RemoveItems ---

    /// <summary>
    /// Passing an empty player list should succeed without removing any rows.
    /// </summary>
    [TestMethod]
    public async Task RemoveItems_EmptyPlayerList_ReturnsSuccessWithNoDeletes()
    {
        _context.SheetsCheatSheetItems.Add(
            new SheetsCheatSheetItem { CheatSheetId = 1, PlayerId = 10, Seqno = 1, Note = string.Empty });
        await _context.SaveChangesAsync();

        var result = await _writer.RemoveItems(1, new List<int>());

        Assert.IsTrue(result.Success);
        Assert.AreEqual(1, _context.SheetsCheatSheetItems.Count());
    }

    /// <summary>
    /// Removing players that exist on the sheet should delete only those rows.
    /// </summary>
    [TestMethod]
    public async Task RemoveItems_ValidPlayers_RemovesOnlyMatchingItems()
    {
        _context.SheetsCheatSheetItems.AddRange(
            new SheetsCheatSheetItem { CheatSheetId = 1, PlayerId = 10, Seqno = 1, Note = string.Empty },
            new SheetsCheatSheetItem { CheatSheetId = 1, PlayerId = 20, Seqno = 2, Note = string.Empty },
            new SheetsCheatSheetItem { CheatSheetId = 1, PlayerId = 30, Seqno = 3, Note = string.Empty });
        await _context.SaveChangesAsync();

        var result = await _writer.RemoveItems(1, new List<int> { 10, 30 });

        Assert.IsTrue(result.Success);
        var remaining = _context.SheetsCheatSheetItems.ToList();
        Assert.AreEqual(1, remaining.Count);
        Assert.AreEqual(20, remaining[0].PlayerId);
    }

    /// <summary>
    /// Removing players that are not on the sheet should succeed without errors.
    /// </summary>
    [TestMethod]
    public async Task RemoveItems_PlayersNotOnSheet_ReturnsSuccess()
    {
        _context.SheetsCheatSheetItems.Add(
            new SheetsCheatSheetItem { CheatSheetId = 1, PlayerId = 10, Seqno = 1, Note = string.Empty });
        await _context.SaveChangesAsync();

        var result = await _writer.RemoveItems(1, new List<int> { 999 });

        Assert.IsTrue(result.Success);
        Assert.AreEqual(1, _context.SheetsCheatSheetItems.Count());
    }
}
