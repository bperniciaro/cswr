using Microsoft.AspNetCore.Mvc;

namespace Cswr.Web.Areas.FantasyFootball.Controllers.Rankings;

    [Area("fantasyfootball")]
    public class RankingsController : Controller
    {

        [Route("/fantasy-football/rankings", Name = "fantasyfootball.rankings")]
        public IActionResult AllPlayers()
        {
            return View();
        }

        [Route("/fantasy-football/rankings/{positionCode}", Name = "fantasyfootball.rankings.positional")]
        public IActionResult Positional([FromRoute] string positionCode)
        {
            return View((object)positionCode);
        }
    }
