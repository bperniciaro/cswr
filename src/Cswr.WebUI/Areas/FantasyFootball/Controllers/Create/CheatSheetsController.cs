using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cswr.BLL;


namespace Cswr.Web.Areas.FantasyFootball.Controllers.Create
{
    [Area("fantasyfootball")]
    public class CheatSheetsController : Controller
    {
        private readonly ICheatSheetService _repository;

        public CheatSheetsController(ICheatSheetService repository)
        {
            _repository = repository;
        }

        [Route("fantasy-football/cheatsheets/manage", Name = "create.cheatsheets.manage")]
        public IActionResult Manage()
        {
            var testCheatSheets = _repository.GetCheatSheets();
            return View(testCheatSheets);
        }

        [Route("/fantasy-football/cheatsheets/configureprint", Name = "create.cheatsheets.configureprint")]
        public IActionResult ConfigurePrint()
        {
            return View();
        }
    }
}
