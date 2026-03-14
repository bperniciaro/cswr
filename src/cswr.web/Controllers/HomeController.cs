using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspStudio.Models;
using Cswr.Bal.Services;
using Cswr.Web.Lib.Config;
using Cswr.Bal.Domain;
using Cswr.Bal.Services.Interfaces.Readers;

namespace AspStudio.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;
    private readonly ICheatSheetReader cheatSheetReader;
    private readonly SmtpMailConfig smtpMailConfig;
    private readonly SocialLoginConfig socialLoginConfig;

    public HomeController(ICheatSheetReader cheatSheetReader, ILogger<HomeController> logger,
        SmtpMailConfig smtpMailConfig, SocialLoginConfig socialLoginConfig)
    {
        this.cheatSheetReader = cheatSheetReader;
        this.logger = logger;

        this.smtpMailConfig = smtpMailConfig;
        this.socialLoginConfig = socialLoginConfig;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await this.cheatSheetReader.GetCheatSheets(this.User.Identity!.Name!);
        if (result.Failure)
        {
            this.TempData["warning"] = "There was a problem retrieving your sheet";
            return this.View();
        }

        var targetSheets = (List<CheatSheet>?)result.Value;

        if (targetSheets == null || targetSheets?.Count == 0)
        {
            this.TempData["warning"] = "We weren't able to find any sheets";
            return this.View();
        }

        var targetSheet = targetSheets?.First();

        //var z = this.cheatSheetService.GetCheatSheet(1645168);
        ///var z = y.SheetsCheatSheetItems;
        ///

        return this.View();
        //return new RedirectToRouteResult("fantasyfootball.create.cheatsheet.edit", new { id = 1645168 });
    }

    [HttpGet]
    public IActionResult Test()
    {
        this.TempData["success"] = "Test";
        return View("Index");
    }

    [HttpPost]
    [Route("/Home/Test", Name = "hometest")]
    public IActionResult Test(string test)
    {
        this.TempData["danger"] = "Test";
        return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
