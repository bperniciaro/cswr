using Microsoft.AspNetCore.Mvc;

namespace Cswr.Web.Areas.FantasyFootball.Controllers.Custom
{
    [Area("fantasyfootball")]
    public class CheatSheetController : Controller
    {
        [Route("/fantasy-football/create/cheatsheet/custom/rankings/{id?}", Name = "fantasyfootball.create.cheatsheet.rankings")]
        public IActionResult Rankings()
        {
            return View();
        }

        [Route("/fantasy-football/create/cheatsheet/new", Name = "fantasyfootball.create.cheatsheet.create")]
        public IActionResult New()
        {
            return View();
        }

        [Route("/fantasy-football/create/cheatsheet/edit/{id?}", Name = "fantasyfootball.create.cheatsheet.edit")]
        public IActionResult Edit(int id)
        {
            return View(id);
        }

        [Route("/fantasy-football/create/cheatsheet/validate/{id?}", Name = "fantasyfootball.create.cheatsheet.validate")]
        public IActionResult Validate(int id)
        {
            return View(id);
        }

    }
}
