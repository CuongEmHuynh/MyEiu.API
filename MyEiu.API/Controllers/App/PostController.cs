using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyEiu.Application.Services.App.Posts;

namespace MyEiu.API.Controllers.App
{
    public class PostController : APIBaseController
    {
        private readonly IPostService _service;

        public PostController(IPostService service)
        {
            _service = service;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> GetPostsByUser(int userid) => Ok(await _service.GetPostsByUser(userid));

    }
}
