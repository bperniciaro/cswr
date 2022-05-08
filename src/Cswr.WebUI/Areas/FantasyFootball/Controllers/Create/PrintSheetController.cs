using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cswr.Web.Areas.FantasyFootball.Controllers.Create
{
    [Area("fantasyfootball")]
    public class PrintSheetController : Controller
    {

        [Route("/fantasy-football/printsheet/oneposition", Name = "create.printable.cheatsheet.oneposition")]
        public IActionResult OnePosition(int id)
        {
            return View(id);
        }

        [Route("/fantasy-football/printsheet/multiplepositions", Name = "create.printable.cheatsheet.multiplepositions")]
        public IActionResult MultiplePositions(int id)
        {
            return View(id);
        }

        [Route("/fantasy-football/printsheet/withroster", Name = "create.printable.cheatsheet.withroster")]
        public IActionResult WithRoster(int id)
        {
            return View(id);
        }

        [Route("/fantasy-football/printsheet/withoutroster", Name = "create.printable.cheatsheet.withoutroster")]
        public IActionResult WithoutRoster(int id)
        {
            return View(id);
        }

    }
}
