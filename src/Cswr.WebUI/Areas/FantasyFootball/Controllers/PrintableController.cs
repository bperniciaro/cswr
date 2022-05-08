using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cswr.Web.Areas.FantasyFootball.Controllers
{
    [Area("fantasyfootball")]
    public class PrintableController : Controller
    {
        [Route("/fantasy-football/printable/cheatsheets", Name = "printable.cheatsheets")]
        public IActionResult CheatSheets()
        {
            return View();
        }

        [Route("/fantasy-football/printable/cheatsheet/fake", Name = "printable.cheatsheet.fake")]
        public IActionResult Fake()
        {
            return View();
        }

        [Route("/fantasy-football/printable/cheatsheet/quarterbacks", Name = "printable.cheatsheet.quarterbacks")]
        public IActionResult Quarterbacks()
        {
            return View();
        }

        [Route("/fantasy-football/printable/cheatsheet/running-backs", Name = "printable.cheatsheet.runningbacks")]
        public IActionResult RunningBacks()
        {
            return View();
        }

        [Route("/fantasy-football/printable/cheatsheet/wide-receivers", Name = "printable.cheatsheet.widereceivers")]
        public IActionResult WideReceivers()
        {
            return View();
        }

        [Route("/fantasy-football/printable/cheatsheet/tight-ends", Name = "printable.cheatsheet.tightends")]
        public IActionResult TightEnds()
        {
            return View();
        }

        [Route("/fantasy-football/printable/cheatsheet/kickers", Name = "printable.cheatsheet.kickers")]
        public IActionResult Kickers()
        {
            return View();
        }

        [Route("/fantasy-football/printable/cheatsheet/defenses", Name = "printable.cheatsheet.defenses")]
        public IActionResult Defenses()
        {
            return View();
        }
    }
}
