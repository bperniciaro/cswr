using Microsoft.AspNetCore.Mvc;

namespace Cswr.Web.Controllers
{
    public class NotificationsController : Controller
    {
        public IActionResult Sweetalert2() => View();
        public IActionResult Toastr() => View();
    }
}
