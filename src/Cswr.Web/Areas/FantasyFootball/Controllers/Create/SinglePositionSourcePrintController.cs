using Microsoft.AspNetCore.Mvc;

namespace Cswr.Web.Areas.FantasyFootball.Controllers.Custom
{
    [Area("fantasyfootball")]

    public class SinglePositionSourcePrintController : Controller
    {
        [Route("/fantasy-football/create/printable/single-position-source/single-position-print/{id?}", Name = "fantasyfootball.create.print.singlepositionsource.singlepositionprint")]
        public IActionResult SinglePositionPrint(int id)
        {
            return View(id);
        }

        [Route("/fantasy-football/create/printable/single-position-source/multi-position-print/without-roster/{id?}", Name = "fantasyfootball.create.print.singlepositionsource.multipositionprint.withoutroster")]
        public IActionResult MultiPositionPrintWithoutRoster(int id)
        {
            return View(id);
        }

        [Route("/fantasy-football/create/printable/single-position-source/multi-position-print/with-roster/{id?}", Name = "fantasyfootball.create.print.singlepositionsource.multipositionprint.withroster")]
        public IActionResult MultiPositionPrintWithRoster(int id)
        {
            return View(id);
        }


    }
}
