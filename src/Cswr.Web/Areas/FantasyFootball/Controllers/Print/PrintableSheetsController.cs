using Microsoft.AspNetCore.Mvc;

namespace Cswr.Web.Areas.FantasyFootball.Controllers.Print
{
    [Area("fantasyfootball")]
    public class PrintableSheetsController : Controller
    {
        [Route("/fantasy-football/printable/cheatsheets", Name = "fantasyfootball.printable.cheatsheets")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
