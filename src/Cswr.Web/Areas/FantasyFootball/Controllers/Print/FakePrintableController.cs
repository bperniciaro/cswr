using Microsoft.AspNetCore.Mvc;

namespace Cswr.Web.Areas.FantasyFootball.Controllers.Print
{
    [Area("fantasyfootball")]
    public class FakePrintableController : Controller
    {
        [Route("/fantasy-football/printable/cheatsheet/fake", Name = "fantasyfootball.printable.cheatsheet.fake")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
