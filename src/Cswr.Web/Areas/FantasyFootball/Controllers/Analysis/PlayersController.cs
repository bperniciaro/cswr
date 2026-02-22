using Microsoft.AspNetCore.Mvc;

namespace Cswr.Web.Areas.FantasyFootball.Controllers.Analysis;

    [Area("fantasyfootball")]
    public class PlayersController : Controller
    {
        [Route("/fantasy-football/analysis/players/busts", Name = "fantasyfootball.analysis.players.busts")]
        public IActionResult Busts()
        {
            return View();
        }

        [Route("/fantasy-football/analysis/players/sleepers", Name = "fantasyfootball.analysis.players.sleepers")]
        public IActionResult Sleepers()
        {
            return View();
        }
    }
