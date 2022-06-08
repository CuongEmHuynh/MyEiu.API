using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEiu.API.Dtos;
using MyEiu.Automapper.Settings;
using MyEiu.Automapper.ViewModel;
using MyEiu.Data.EF.DbContexts;
using MyEiu.Data.Entities;
using MyEiu.Utilities;

namespace MyEiu.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PostController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly WebEiuDbContext _webeiudbcontext;
        private readonly MapperConfiguration _configMapper;

        public PostController(IMapper mapper, WebEiuDbContext webeiudbcontext,MapperConfiguration configMapper)
        {
            _mapper = mapper;
            _webeiudbcontext = webeiudbcontext;
            _configMapper = configMapper;
        }

        [HttpGet]
        
        public IList<PostViewModel> Latest(string language)
        {
            List<PostViewModel> postViewModelList = new();
            List<Post> result = new();

            result = _webeiudbcontext.Posts.Where(p => p.Post_Status == "publish" && (p.Post_Type == "post" || p.Post_Type == "events")
                                                   && p.Translation.FirstOrDefault().Language_Code == language)
                                                    .Include(p => p.Translation).Include(p => p.ThumbnailWebEius).Include(p => p.UserWebEiu)
                                                   
                                                   .OrderByDescending(rs => rs.Post_Date)
                                                   .Take(5).ToList();

            postViewModelList = _mapper.Map<List<PostViewModel>>(result);

            return postViewModelList;
        }
        [HttpGet]

        public IList<PostViewModel> TenPosts(string posttype, string language)
        {
            List<PostViewModel> postViewModelList = new();
            List<Post> result = new();


            result = _webeiudbcontext.Posts.Where(p => p.Post_Status == "publish" && p.Post_Type == posttype
                                                   && p.Translation.FirstOrDefault().Language_Code == language)
                                                   .Include(p=>p.Translation).Include(p => p.ThumbnailWebEius).Include(p => p.UserWebEiu)                                                   
                                                   .OrderByDescending(rs => rs.Post_Date)
                                                   .Take(10).ToList();

            postViewModelList = _mapper.Map<List<PostViewModel>>(result);

            return postViewModelList;
        }
        [HttpPost]

        public ActionResult PagingPosts(PostPagingDto postpagingdto)
        {
            List<PostViewModel> postViewModelList = new();

            List<Post> result = _webeiudbcontext.Posts.Where(p => p.Post_Status == "publish" && p.Post_Type == postpagingdto.Post_Type
                                                     && p.Translation.FirstOrDefault().Language_Code == postpagingdto.Post_Language)
                                                    .Include(p=>p.Translation).Include(p => p.ThumbnailWebEius).Include(p => p.UserWebEiu)                                                    
                                                     .OrderByDescending(rs => rs.Post_Date)
                                                     .ToList();

            postViewModelList = _mapper.Map<List<PostViewModel>>(result);
          
            var paginResult = PageUtility.ToPaginationAsync<PostViewModel>(postViewModelList, postpagingdto.Current_Page, postpagingdto.Page_Size);




            return Ok(paginResult);
        }
    }
}
