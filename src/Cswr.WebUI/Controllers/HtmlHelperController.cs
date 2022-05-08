using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Cswr.DAL.Models;
using Cswr.DAL.Models;

namespace Cswr.Web.Controllers
{
    public class HtmlHelperController : Controller
    {
        public IActionResult Index()
        {
            var myCheatSheet = new CheatSheet() { CheatSheetId = 1, SheetName = "Name1"};
            //ViewBag.Test = "my bag contents";
            return View(myCheatSheet);
        }
    }
}
