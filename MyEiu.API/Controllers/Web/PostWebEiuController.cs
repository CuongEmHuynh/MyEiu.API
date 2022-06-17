using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public IList<PostWebViewModel> Latest(string language)
        {
            var result = _webeiudbcontext.Posts!.Where(p => p.Post_Status == "publish" && (p.Post_Type == "post" || p.Post_Type == "events")
                                                   && p.TranslationWebEiu!.Language_Code == language)
                                                    .Include(p => p.ThumbnailWebEiu)
                                                    .Include(p => p.UserWebEiu)

                                                   .OrderByDescending(rs => rs.Post_Date)
                                                   .Take(5);

            var postViewModelList = _mapper.Map<List<PostWebViewModel>>(result);

            return postViewModelList;
        }
        [HttpGet]

        public IList<PostWebViewModel> TenPosts(string posttype, string language)
        {
            var result = _webeiudbcontext.Posts!.Where(p => p.Post_Status == "publish" && p.Post_Type == posttype
                                                   && p.TranslationWebEiu!.Language_Code == language)
                                                   .Include(p => p.ThumbnailWebEiu).Include(p => p.UserWebEiu)
                                                   .OrderByDescending(rs => rs.Post_Date)
                                                   .Take(10);
            if(posttype == "all")
            {
                result = _webeiudbcontext.Posts!.Where(p => p.Post_Status == "publish" && (p.Post_Type == "post" || p.Post_Type == "events")
                                                  && p.TranslationWebEiu!.Language_Code == language)
                                                  .Include(p => p.ThumbnailWebEiu).Include(p => p.UserWebEiu)
                                                  .OrderByDescending(rs => rs.Post_Date)
                                                  .Take(10);
            }
            var postViewModelList = _mapper.Map<List<PostWebViewModel>>(result);

            return postViewModelList;
        }
        [HttpPost]

        public async Task<ActionResult> PagingPosts(PostPagingDto postpagingdto)
        {           
            var query = _webeiudbcontext.Posts!.AsQueryable().Where(p => p.Post_Status == "publish"
                                                 && p.TranslationWebEiu!.Language_Code == postpagingdto.Post_Language);

            var result = query.Where(p => p.Post_Type == postpagingdto.Post_Type);
            if (postpagingdto.Post_Type == "all")
                result = query.Where(x => x.Post_Type == "post" || x.Post_Type == "events");

            var pagingResult = await result.OrderByDescending(x => x.Post_Date).AsQueryable()
                                            .ProjectTo<PostWebViewModel>(_configMapper)
                                            .ToPaginationAsync(postpagingdto.Current_Page, postpagingdto.Page_Size);
            return Ok(pagingResult);
        }
    }
}
