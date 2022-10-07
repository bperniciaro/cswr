using Microsoft.AspNetCore.Mvc;

namespace cswr.web.Areas.FantasyFootball.Controllers.Custom
{
    [Area("fantasyfootball")]

    public class CheatSheetsController : Controller
    {
        [Route("/fantasy-football/create/cheatsheets/configure-print", Name = "fantasyfootball.create.cheatsheets.configureprint")]
        public IActionResult ConfigurePrint(int id)
        {
            return View(id);
        }


        [Route("/fantasy-football/create/cheatsheets/manage", Name = "fantasyfootball.create.cheatsheets.manage")]
        public IActionResult Manage(int id)
        {
            return View();
        }
    }
}
