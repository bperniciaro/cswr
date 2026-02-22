using Microsoft.AspNetCore.Mvc;

namespace Cswr.Web.Areas.FantasyFootball.Controllers.Print;

    [Area("fantasyfootball")]
    public class PrintPositionController : Controller
    {
        [Route("/fantasy-football/printable/cheatsheet/{positionCode}", Name = "fantasyfootball.printable.cheatsheet.positional")]
        public IActionResult Positional([FromRoute] string positionCode)
        {
            return View((object)positionCode);
        }
    }
