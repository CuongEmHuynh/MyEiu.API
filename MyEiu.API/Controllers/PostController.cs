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
        public IList<PostViewModel> LoadDataFromMyEiuDb(PostType _ptype,Language _lng)
        {
            List<PostViewModel> postViewModelList = new();

            List<Post> result = new();
            //List<UserWebEiu> users = _webeiudbcontext.UserWebEius.ToList();

            switch (_ptype)
            {
                case PostType.News:
                    result = _webeiudbcontext.Posts.Where(p => p.Post_Type == "post")
                        .OrderByDescending(rs => rs.Post_Date).Take(5).ToList();
                    break;
                case PostType.Events:
                    result = _webeiudbcontext.Posts.Where(p => p.Post_Type == "events")
                        .OrderByDescending(rs => rs.Post_Date).Take(5).ToList();
                    break;
            }

            switch (_lng)
            {
                case Language.Vietnamese:
                    if( _ptype == PostType.News)
                    {
                        result = result.Where(p => p.Post_Status == "publish"
                                                     && p.Guid.Contains("https://eiu.edu.vn/news/") == false)
                                                    .OrderByDescending(rs => rs.Post_Date)
                                                    .Take(5).ToList();
                    }                    
                    break;
                case Language.English:
                    if (_ptype == PostType.News)
                    {
                        result = result.Where(p => p.Post_Status == "publish"
                                                            && p.Guid.Contains("https://eiu.edu.vn/news/") == true)
                                                   .OrderByDescending(rs => rs.Post_Date).Take(5).ToList();
                    }                       
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
