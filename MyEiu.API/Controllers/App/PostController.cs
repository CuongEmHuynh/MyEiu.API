using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyEiu.API.Dtos;
using MyEiu.Application.Const;
using MyEiu.Application.Dtos;
using MyEiu.Application.Extensions;
using MyEiu.Application.Services.App.Posts;
using MyEiu.Automapper.ViewModel.App.Posts;
using MyEiu.Data.EF.DbContexts;
using MyEiu.Data.Entities.App;
using MyEiu.Utilities.Dtos;

namespace MyEiu.API.Controllers.App
{
    public class PostController : APIBaseController
    {
        private readonly IPostService _service;
        private readonly MobileAppDbContext _context;
        private OperationResult _operationResult;
        private readonly HttpClient _client;
        public PostController(IPostService service, MobileAppDbContext context, HttpClient httpClient)
        {
            _service = service;
            _context = context;
            _client = httpClient;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetPostsByUser(int userid) => Ok(await _service.GetPostsByUser(userid));
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Add([FromBody] PostViewModel model) => Ok(await _service.AddAsync(model));

        [HttpPost]
        [AllowAnonymous]
        public async Task<OperationResult> AddPostType(PostType posttypedto)
        {
            try
            {
                _context.PostTypes!.Add(posttypedto);
               await _context.SaveChangesAsync();
                _operationResult = new OperationResult()
                {
                    StatusCode = StatusCodee.Ok,
                    Message = MessageReponse.AddSuccess,
                    Success = true,
                    Data = posttypedto
                };
            }
            catch(Exception ex)
            {
                _operationResult = ex.GetMessageError();
            }
           return _operationResult;
        }

        [HttpPost]
        public async Task<OperationResult> PushNoti(int postid)
        {
            return await _service.PushNoti(postid);
            
        }
        [HttpGet]
        public async Task<OperationResult> NotiDetails(int postid)
        {
            return await _service.NotiDetails(postid);
            
        }
        [HttpGet]
        public async Task<OperationResult> NotiListByUser(string email)
        {
            return await _service.NotiListByUser(email);

        }
        [HttpPost]
        public async Task<ActionResult> PagingNoti(PostAppPagingDto pagingdto)
        {
            return Ok( await _service.PagingNoti(pagingdto));

        }
    }
}
