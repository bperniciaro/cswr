using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cswr.Web.Areas.FantasyFootball.Controllers
{
    [Area("fantasyfootball")]
    public class RankingsController : Controller
    {
        [Route("/fantasy-football/rankings/players", Name = "rankings.player")]
        public IActionResult Players()
        {
            return View();
        }

        [Route("/fantasy-football/rankings/defense", Name = "rankings.defense")]
        public IActionResult Defense()
        {
            return View();
        }

        [Route("/fantasy-football/rankings/kicker", Name = "rankings.kicker")]
        public IActionResult Kicker()
        {
            return View();
        }

        [Route("/fantasy-football/rankings/quarterback", Name = "rankings.quarterback")]
        public IActionResult Quarterback()
        {
            return View();
        }

        [Route("/fantasy-football/rankings/tightend", Name = "rankings.tightend")]
        public IActionResult TightEnd()
        {
            return View();
        }

        [Route("/fantasy-football/rankings/runningback", Name = "rankings.runningback")]
        public IActionResult RunningBack()
        {
            return View();
        }

        [Route("/fantasy-football/rankings/widereceiver", Name = "rankings.widereceiver")]
        public IActionResult WideReceiver()
        {
            return View();
        }

        [Route("/fantasy-football/rankings/busts", Name = "rankings.bust")]
        public IActionResult Busts()
        {
            return View();
        }

        [Route("/fantasy-football/rankings/sleepers", Name = "rankings.sleeper")]
        public IActionResult Sleepers()
        {
            return View();
        }

        [Route("/fantasy-football/rankings/adp", Name = "rankings.adp")]
        public IActionResult Adp()
        {
            return View();
        }

    }
}
