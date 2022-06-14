using Microsoft.AspNetCore.Mvc;

namespace MyEiu.API.Controllers.App
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
