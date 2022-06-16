using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEiu.API.Dtos;
using MyEiu.Automapper.ViewModel.Web;
using MyEiu.Data.EF.DbContexts;
using MyEiu.Data.Entities.Web;
using MyEiu.Utilities;

namespace MyEiu.API.Controllers.Web
{
    public class PostWebEiuController : APIBaseController
    {
        private readonly IMapper _mapper;
        private readonly WebEiuDbContext _webeiudbcontext;
        private readonly MapperConfiguration _configMapper;

        public PostWebEiuController(IMapper mapper, WebEiuDbContext webeiudbcontext, MapperConfiguration configMapper)
        {
            _mapper = mapper;
            _webeiudbcontext = webeiudbcontext;
            _configMapper = configMapper;
        }

        [HttpGet]

        public async Task<IList<PostWebViewModel>> Latest(string language)
        {
            List<PostWebViewModel> postViewModelList = new();
            List<PostWebEiu> result = new();

            result = await _webeiudbcontext.Posts.Where(p => p.Post_Status == "publish" && (p.Post_Type == "post" || p.Post_Type == "events")
                                                   && p.TranslationWebEiu.Language_Code == language)
                                                    .Include(p => p.ThumbnailWebEiu)
                                                    .Include(p => p.UserWebEiu)

                                                   .OrderByDescending(rs => rs.Post_Date)
                                                   .Take(5).ToListAsync();

            postViewModelList = _mapper.Map<List<PostWebViewModel>>(result);

            return postViewModelList;
        }
        [HttpGet]

        public async Task<IList<PostWebViewModel>> TenPosts(string posttype, string language)
        {
            List<PostWebViewModel> postViewModelList = new();
            List<PostWebEiu> result = new();

            if (posttype != "all")
            {
                result = await _webeiudbcontext.Posts.Where(p => p.Post_Status == "publish" && p.Post_Type == posttype
                                                   && p.TranslationWebEiu.Language_Code == language)
                                                   //.Include(p => p.ThumbnailWebEiu).Include(p => p.UserWebEiu)
                                                   .OrderByDescending(rs => rs.Post_Date)
                                                   .Take(10).ToListAsync();
            }
            else
            {
                result = await _webeiudbcontext.Posts.Where(p => p.Post_Status == "publish" && (p.Post_Type == "post" || p.Post_Type == "events")
                                                  && p.TranslationWebEiu.Language_Code == language)
                                                  //.Include(p => p.ThumbnailWebEiu).Include(p => p.UserWebEiu)
                                                  .OrderByDescending(rs => rs.Post_Date)
                                                  .Take(10).ToListAsync();
            }


            postViewModelList = _mapper.Map<List<PostWebViewModel>>(result);

            return postViewModelList;
        }
        [HttpPost]

        public async Task<ActionResult> PagingPosts(PostPagingDto postpagingdto)
        {
            List<PostWebViewModel> postViewModelList = new();

            List<PostWebEiu> result = new();

            if (postpagingdto.Post_Type != "all")
            {
                result = await _webeiudbcontext.Posts.Where(p => p.Post_Status == "publish" && p.Post_Type == postpagingdto.Post_Type
                                                    && p.TranslationWebEiu.Language_Code == postpagingdto.Post_Language)
                                                    //.Include(p => p.ThumbnailWebEiu).Include(p => p.UserWebEiu)
                                                     .OrderByDescending(rs => rs.Post_Date)
                                                     .ToListAsync();
            }
            else
            {
                result = await _webeiudbcontext.Posts.Where(p => p.Post_Status == "publish" && (p.Post_Type == "post" || p.Post_Type == "events")
                                                    && p.TranslationWebEiu.Language_Code == postpagingdto.Post_Language)
                                                    //.Include(p => p.ThumbnailWebEiu).Include(p => p.UserWebEiu)
                                                     .OrderByDescending(rs => rs.Post_Date)
                                                     .ToListAsync();
            }


            postViewModelList = _mapper.Map<List<PostWebViewModel>>(result);

            var pagingResult = postViewModelList.ToPaginationAsync(postpagingdto.Current_Page, postpagingdto.Page_Size);




            return Ok(pagingResult);
        }
    }
}
