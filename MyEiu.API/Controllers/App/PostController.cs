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
        public async Task<ActionResult> Add([FromBody] PostViewModel model)
        {
            model.CreateDate = DateTime.Now;
            return   Ok(await _service.AddAsync(model));
        }
        [HttpPost]
        public async Task<OperationResult> AddPush([FromBody] PostViewModel model)
        {
            return await _service.AddPush(model);
            
        }

        [HttpGet]
        public async Task<OperationResult> PushNoti(int postid)
        {
            return await _service.PushNoti(postid);
        }
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

        
        [HttpGet]
        public async Task<OperationResult> NotiDetails(int postid,string email)
        {
            return await _service.NotiDetails(postid,email);
            
        }
        [HttpPost]
        public async Task<OperationResult> NotiListByUser( NotifPagingDto pagingdto)
        {
            return await _service.NotiListByUser(pagingdto);

        }
        [HttpGet]
        public async Task<OperationResult> CountNewNotifUser(string email)
        {
            return await _service.CountNewNotifUser(email);

        }
        [HttpPost]
        public async Task<ActionResult> PagingNoti(PostAppPagingDto pagingdto)
        {
            return Ok( await _service.PagingNoti(pagingdto));

        }
    }
}
