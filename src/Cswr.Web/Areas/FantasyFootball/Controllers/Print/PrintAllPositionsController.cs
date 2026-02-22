using Microsoft.AspNetCore.Mvc;

namespace Cswr.Web.Areas.FantasyFootball.Controllers.Print;

    [Area("fantasyfootball")]
    public class PrintAllPositionsController : Controller
    {
        [Route("/fantasy-football/printable/cheatsheet/all-positions/with-roster", Name = "fantasyfootball.printable.cheatsheet.allpositions.withroster")]
        public IActionResult WithRoster()
        {
            return View();
        }

        [Route("/fantasy-football/printable/cheatsheet/all-positions/without-roster", Name = "fantasyfootball.printable.cheatsheet.allpositions.withoutroster")]
        public IActionResult WithoutRoster()
        {
            return View();
        }
    }
