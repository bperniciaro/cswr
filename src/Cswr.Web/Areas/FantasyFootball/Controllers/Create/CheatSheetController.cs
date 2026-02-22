// <copyright file="CheatSheetController.cs" company="Fornits Web Solutions">
// Copyright (c) Fornits Web Solutions. All rights reserved.
// </copyright>

namespace Cswr.Web.Areas.FantasyFootball.Controllers.Custom;

using AspStudio.Controllers;
using AutoMapper;
using Cswr.Bal.Domain;
using Cswr.Bal.Services.Interfaces.Readers;
using Cswr.Bal.Services.Interfaces.Writers;
using Cswr.Web.Lib;
using Cswr.Web.Models;
using Microsoft.AspNetCore.Mvc;
using static Cswr.Web.Models.EditSheetViewModel;

[Area("fantasyfootball")]
public class CheatSheetController : Controller
{
    private readonly ILogger<HomeController> logger;
    private readonly ICheatSheetReader cheatSheetReader;
    private readonly ICheatSheetWriter cheatSheetWriter;
    private readonly IMapper mapper;

    private readonly ICheatSheetItemReader cheatSheetItemReader;
    private readonly IPlayerReader plyerReader;


    /// <summary>
    /// Initializes a new instance of the <see cref="CheatSheetController"/> class.
    /// </summary>
    /// <param name="cheatSheetReader">The service responsible for reading cheat sheet data.</param>
    /// <param name="cheatSheetWriter">The service responsible for writing cheat sheet data.</param>
    /// <param name="logger">The service responsible for logging information.</param>
    public CheatSheetController(ICheatSheetReader cheatSheetReader, ICheatSheetWriter cheatSheetWriter, 
        ILogger<HomeController> logger, IMapper mapper, ICheatSheetItemReader cheatSheetItemReader, IPlayerReader plyerReader)
    {
        this.cheatSheetReader = cheatSheetReader;
        this.logger = logger;
        this.cheatSheetWriter = cheatSheetWriter;
        this.mapper = mapper;

        this.cheatSheetItemReader = cheatSheetItemReader;
        this.plyerReader = plyerReader;
    }

    [Route("/fantasy-football/create/cheatsheet/custom/rankings/{id?}", Name = "fantasyfootball.create.cheatsheet.rankings")]
    public IActionResult Custom()
    {
        return View();
    }

    [Route("/fantasy-football/create/cheatsheet/new", Name = "fantasyfootball.create.cheatsheet.create")]
    public IActionResult New()
    {
        return View();
    }

    /// <summary>
    /// Action method used to populate a form for editing a single sheet.
    /// </summary>
    /// <param name="id">The unique id of the taget sheet.</param>
    /// <returns>A view containing the sheet to be edited.</returns>
    [HttpGet]
    [Route("/fantasy-football/create/cheatsheet/edit/{id?}", Name = "fantasyfootball.create.cheatsheet.edit")]
    public async Task<IActionResult> Edit(int id)
    {
        this.logger.LogError($"Action: {nameof(CheatSheetController)}.{nameof(this.Edit)}, Parameters: {nameof(id)} - {id}");

        EditSheetViewModel model = new ();

        if (!this.ModelState.IsValid)
        {
            this.TempData[Enumerations.MessageLevel.Error.ToString()] = "Sheet cannot be found.";
            this.logger.LogError($"Event Action: Model state determined to be invalid.");
            return this.View(model);
        }

        // Get the cheat sheet properties
        var targetSheetResult = await this.cheatSheetReader.GetCheatSheet(id);
        if (targetSheetResult.Failure)
        {
            this.TempData[Enumerations.MessageLevel.Error.ToString()] = "There was a problem retrieving your cheat sheet.";
            this.logger.LogError($"Event Action: Unable to populate cheat sheet: {id}.");
            return this.View(model);
        }

        // Get the cheat sheet items
        var itemsResult = await this.cheatSheetItemReader.GetCheatSheetItems(id);
        if (itemsResult.Failure)
        {
            this.TempData[Enumerations.MessageLevel.Error.ToString()] = $"There was a problem retrieving the sheet items.";
            this.logger.LogError($"Event Action: Problem loading cheat sheet items: {id}.");
            return this.View(model);
        }

        // Get the available players
        var availablePlayersResult = await this.plyerReader.GetPlayersNotInSheet(1649857);
        if (availablePlayersResult.Failure)
        {
            this.TempData[Enumerations.MessageLevel.Error.ToString()] = $"There was a problem retrieving the available players.";
            this.logger.LogError($"Event Action: Problem loading cheat sheet available players: {id}");
            return this.View(model);
        }

        if (targetSheetResult.Value is null || itemsResult.Value is null || availablePlayersResult.Value is null)
        {
            this.TempData[Enumerations.MessageLevel.Error.ToString()] = $"There was a problem retrieving data for this page.";
            this.logger.LogError($"Problem loading cheat sheet available players: {id}");
            return this.View(model);
        }

        model.CheatSheetId = targetSheetResult.Value.CheatSheetId;
        model.UserName = targetSheetResult.Value.UserName;
        model.SheetName = targetSheetResult.Value.SheetName;
        model.SportCode = targetSheetResult.Value.SportCode;
        model.SeasonCode = targetSheetResult.Value.SeasonCode;
        model.StatsSeasonCode = targetSheetResult.Value.StatsSeasonCode;
        model.PprLeague = targetSheetResult.Value.PprLeague;
        model.AuctionDraft = targetSheetResult.Value.AuctionDraft;

        model.CurrentSheetItems = itemsResult.Value.Select(x => new CheatSheetItemDetails() { PlayerId = x.PlayerId, FullName = x.Player.FullName }).ToList();
        model.AvailablePlayers = availablePlayersResult.Value.Select(x => new PlayerDetails() { PlayerId = x.PlayerId, FullName = x.FullName }).ToList();

        return this.View(model);
    }

    /// <summary>
    /// Save the properties of the cheat sheet.
    /// </summary>
    /// <param name="model">The cheat sheet model to be saved.</param>
    /// <returns>The edit sheet view with the appropriate user message.</returns>
    [HttpPost]
    [Route("/fantasy-football/create/cheatsheet/savesheet", Name = "fantasyfootball.create.cheatsheet.savesheet")]
    public IActionResult SaveSheet(EditSheetViewModel model)
    {
        if (!this.ModelState.IsValid)
        {
            this.TempData["error"] = "Sheeet not saved, unexpected error.";
            return this.View("Edit", model);
        }

        // Try to update the cheat sheet
        var mappedCheatSheet = this.mapper.Map<CheatSheet>(model);
        var updateResult = this.cheatSheetWriter.UpdateCheatSheet(mappedCheatSheet).Result;

        if (updateResult.Success)
        {
            this.TempData["success"] = "Sheet saved successfully";
        }

        this.logger.LogError($"Unable to save cheat sheet, cheatSheetId: {model.CheatSheetId}");
        this.TempData["error"] = "Sheeet not saved, unexpected error.";
        return this.View("Edit", model);
    }

    [HttpPost]
    [Route("/fantasy-football/create/cheatsheet/addplayers", Name = "fantasyfootball.create.cheatsheet.addplayers")]
    public ActionResult AddPlayers([FromForm] EditSheetViewModel model)
    {
        if (!this.ModelState.IsValid)
        {
            this.TempData["error"] = "Updates not saved, unexpected error.";
        }

        if (!model.PlayersToAdd.Any())
        {
            this.TempData["warning"] = "No players were selected for addition.";
        }



        return RedirectToRoute("fantasyfootball.create.cheatsheet.edit", new { Id = model.CheatSheetId });
    }

    [HttpPost]
    [Route("/fantasy-football/create/cheatsheet/removeplayers", Name = "fantasyfootball.create.cheatsheet.removeplayers")]
    public ActionResult RemovePlayers([FromForm] EditSheetViewModel model)
    {
        return RedirectToRoute("fantasyfootball.create.cheatsheet.edit", new { Id = model.CheatSheetId });
    }

    [Route("/fantasy-football/create/cheatsheet/validate/{id?}", Name = "fantasyfootball.create.cheatsheet.validate")]
    public IActionResult Validate(int id)
    {
        return View(id);
    }

}
