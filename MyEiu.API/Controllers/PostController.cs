using Microsoft.AspNetCore.Mvc;

namespace MyEiu.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PostController : ControllerBase
    {

        [HttpGet]
        public IList<PostViewModel> LoadDataFromMyEiuDb()
        {
            //return _mySqlDbContext.Students.ToList();
            List<PostViewModel> postViewModel = new List<PostViewModel>();

            var kq = _eiuDbContext.Posts.OrderByDescending(rs => rs.Post_Date).Take<Post>(5).ToList();

            return postViewModel;
        }
    }
}
