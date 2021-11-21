using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cswr.Web.Areas.FantasyFootball.Controllers.Create
{
    [Area("fantasyfootball")]
    public class CustomController : Controller
    {
        [Route("/fantasy-football/cheatsheet/custom/rankings/{id?}", Name = "create.custom.cheatsheet")]
        public IActionResult Rankings()
        {
            return View();
        }
    }
}
