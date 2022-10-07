using Microsoft.AspNetCore.Mvc;

namespace Cswr.Web.Areas.FantasyFootball.Controllers.Custom
{
    [Area("fantasyfootball")]
    public class MultiPositionSourcePrintController : Controller
    {

        [Route("/fantasy-football/create/printable/multi-position-source/multi-position-print/with-roster/{id?}", Name = "fantasyfootball.create.print.multipositionsource.multipositionprint.withroster")]
        public IActionResult MultiPositionPrintWithRoster(int id)
        {
            return View(id);
        }
    }
}
