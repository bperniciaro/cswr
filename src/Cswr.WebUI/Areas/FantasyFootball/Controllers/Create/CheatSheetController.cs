using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cswr.Web.Areas.FantasyFootball.Controllers.Create
{
    [Area("fantasyfootball")]
    public class CheatSheetController : Controller
    {
        [Route("/fantasy-football/cheatsheet/new", Name = "create.cheatsheet.new")]
        public IActionResult New()
        {
            return View();
        }

        [Route("/fantasy-football/cheatsheet/edit/{id?}", Name = "create.cheatsheet.edit")]
        public IActionResult Edit(int id)
        {
            return View(id);
        }

        [Route("/fantasy-football/cheatsheet/validate/{id?}", Name = "create.cheatsheet.validate")]
        public IActionResult Validate(int id)
        {
            return View(id);
        }
    }
}
