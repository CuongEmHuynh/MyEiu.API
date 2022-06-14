using Microsoft.AspNetCore.Mvc;

namespace MyEiu.API.Controllers.App
{
    public class GroupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
