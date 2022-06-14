using Microsoft.AspNetCore.Mvc;

namespace MyEiu.API.Controllers.App
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
