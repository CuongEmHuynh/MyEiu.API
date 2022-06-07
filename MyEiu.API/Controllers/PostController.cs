using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyEiu.Automapper.Settings;
using MyEiu.Automapper.ViewModel;
using MyEiu.Data.EF.DbContexts;
using MyEiu.Data.Entities;
using MyEiu.Data.Enum;

namespace MyEiu.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PostController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly WebEiuDbContext _webeiudbcontext;
        public PostController(IMapper mapper, WebEiuDbContext webeiudbcontext)
        {
            _mapper = mapper;
            _webeiudbcontext = webeiudbcontext;
        }

        [HttpGet]
        public IList<PostViewModel> LoadDataFromMyEiuDb(Language _lng)
        {
            //return _mySqlDbContext.Students.ToList();
            List<PostViewModel> postViewModelList = new();

            List<Post> result = new List<Post>();
            IList<UserWebEiu> userWebEius = _webeiudbcontext.UserWebEius.ToList();


            switch(_lng)
            {
                case Language.Vietnamese:
                    result = _webeiudbcontext.Posts.Where(p => p.Post_Type == "post" && p.Post_Status == "publish"
                                                            && p.Ping_Status == "open" && p.Guid.Contains("https://eiu.edu.vn/news/") == false)
                                                    //.Join(userWebEius, post => post.Post_Author, nd => nd.Id,
                                                    //    (post, nd) => new
                                                    //    {

                                                    //        AuthorName = nd.Dislay_Name,

                                                    //    })
                                                    .OrderByDescending(rs => rs.Post_Date)
                                                    
                                                    .Take(5).ToList();
                    break;
                case Language.English:
                    result = _webeiudbcontext.Posts.Where(p => p.Post_Type == "post" && p.Post_Status == "publish"
                                                            && p.Ping_Status == "open" && p.Guid.Contains("https://eiu.edu.vn/news/") == true)
                                                    .OrderByDescending(rs => rs.Post_Date).Take<Post>(5..2).ToList();
                    break;
            }
            



            foreach(var post in result)
            {
                PostViewModel postViewModel = new PostViewModel();
                postViewModel = _mapper.Map<PostViewModel>(post);
                postViewModelList.Add(postViewModel);
            }

            return postViewModelList;
        }
    }
}
