using Microsoft.AspNetCore.Mvc;

namespace Cswr.Web.Controllers
{
    public class TablesController : Controller
    {
        public IActionResult Basic() => View();
        public IActionResult GenerateStyle() => View();
    }
}
