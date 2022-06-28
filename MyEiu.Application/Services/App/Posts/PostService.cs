using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyEiu.API.Dtos;
using MyEiu.Application.Const;
using MyEiu.Application.Dtos;
using MyEiu.Application.Extensions;
using MyEiu.Automapper.ViewModel.App.Notification;
using MyEiu.Automapper.ViewModel.App.Posts;
using MyEiu.Data.EF.DbContexts;
using MyEiu.Data.EF.Interface;
using MyEiu.Data.Entities.App;
using MyEiu.Utilities;
using MyEiu.Utilities.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Application.Services.App.Posts
{
    public interface IPostService : IBaseService<PostViewModel>
    {
        //Task<OperationResult> Add(PostViewModel model,FileDataViewModel f_model);       
        Task<OperationResult> AddPush(PostViewModel model);
        Task<OperationResult> PushNoti(int id);
        Task<OperationResult> Update(PostViewModel model);
        Task<List<PostViewModel>> GetPostsByUser(int userid);

        Task<OperationResult> NotiListByUser(string email);
        Task<OperationResult> CountNewNotifUser(string email);
        Task<OperationResult> NotiDetails(int postid,string email);//user view notification in details           
        Task<Pager> PagingNoti(PostAppPagingDto pagingdto);

    }
    public class PostService : BaseService<Post, PostViewModel>, IPostService
    {
        private readonly IRepository<Post> _repoPost;
        private readonly IRepository<PostUser> _repoPostUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private OperationResult? operationResult;
        private readonly HttpClient _httpClient;
        private readonly StaffEiuDbContext _staffEiuDbContext;

        public PostService(IRepository<Post> postRepository, IRepository<PostUser> repoPostUser, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper, 
            HttpClient httpClient,StaffEiuDbContext staffEiuDbContext)
            : base(postRepository, unitOfWork, mapper, configMapper)
        {
            _repoPost = postRepository;
            _repoPostUser = repoPostUser;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
            _httpClient = httpClient;
            _staffEiuDbContext = staffEiuDbContext;
        }

        public async Task<List<PostViewModel>> GetPostsByUser(int userid)
        {
            var item = await _repoPost.FindAll(p => p.CreateBy == userid).Include(p => p.Author).Include(p=>p.Editor).ToListAsync();
            return _mapper.Map<List<PostViewModel>>(item);
        }
        
        public async Task<OperationResult> AddPush(PostViewModel model)
        {
            var post = _mapper.Map<Post>(model);
            try
            {
                await _repoPost.AddAsync(post);
                await _unitOfWork.SaveChangeAsync();
                operationResult = new OperationResult()
                {
                    Message = "Check lưu thành công"
                };
                operationResult = await PushNoti(post.Id);               
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }
        public async Task<OperationResult> PushNoti(int postid)
        {
            try
            {
                NotificationDto notif = new NotificationDto();
                //get all relevant emails with post
                //Post item = await _mobileAppDbContext.Posts.Where(p => p.Id == postid).Include(p => p.PostGroups).Include(p => p.PostUsers).FirstOrDefaultAsync();
                Post item = await _repoPost.FindAll(p => p.Id == postid).Include(p => p.PostGroups).Include(p => p.PostUsers).FirstOrDefaultAsync();
                if (item != null)
                {
                    notif.Type = item.PostTypeId;

                    foreach (var p in item.PostUsers!)
                    {
                        notif.Emails!.Add(p.Email!);
                    }

                    foreach (var p in item.PostGroups!)
                    {
                        var temp = await _staffEiuDbContext.StaffEius!.Where(d => d.IsDeleted == 0 && d.Type != 4 && d.DepartmentEiu!.RecordID == p.GroupId).Select(s => s.SchoolEmail).ToListAsync();
                        if (temp != null)
                        {
                            notif.Emails!.AddRange(temp!);
                        }
                    }

                    //declare data
                    notif.Data!.Title = item.Title;
                    notif.Data.Body = item.Description;
                    notif.Data.PostId = item.Id;
                }

                _httpClient.DefaultRequestHeaders.Add("IDCAppApiKey", "IDC@123456");
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://api.becamex.com.vn/eiu/sys/push-notif", notif);

                response.EnsureSuccessStatusCode();// make sure return ok, fail go to Catch Exception
                string apiResponse = await response.Content.ReadAsStringAsync();

                //change status post from Draft to delivered
                item.Status = Data.Enum.PostStatus.Delivered;
                _repoPost.Update(item);
                await _unitOfWork.SaveChangeAsync();

                operationResult = new OperationResult()
                {
                    Data = notif,
                    Message = apiResponse,
                    StatusCode = 200,
                    Success = true
                };


            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }
        public Task<OperationResult> Update(PostViewModel model)
        {
            throw new NotImplementedException();
        }
        public async Task<OperationResult> CountNewNotifUser(string email)
        {
            try
            {
                //get all relevant emails with post
                var num = await _repoPostUser.FindAll(pu => pu.Email == email && pu.Status == Data.Enum.PostStatus.New).CountAsync();


                if (num != null)
                {                    
                    operationResult = new OperationResult()
                    {
                        Data = num,
                        Message = "Get data success",
                        StatusCode = StatusCodee.Ok,
                        Success = true
                    };
                }
                else
                {
                    operationResult = new OperationResult()
                    {
                        Data = 0,
                        Message = "No record found",
                        StatusCode = StatusCodee.NotFound,
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }
       

        public async Task<OperationResult> NotiDetails(int postid, string email)
        {
            try
            {
                //get all relevant emails with post
                Post item = await _repoPost.FindAll(p => p.Id == postid && p.Disable == false).Include(p => p.Author)
                                                        .Include(p => p.PostFileDatas).ThenInclude(pf => pf.FileData)
                                                        .FirstOrDefaultAsync();

                if (item != null)
                {

                    NotificationViewModel notifmodel = _mapper.Map<NotificationViewModel>(item);
                    foreach(var pfiledata in item.PostFileDatas!)
                    {
                        notifmodel.FilesUrl.Add(new NotifFile { Name = pfiledata.FileData.DisplayName, Path = "/" + pfiledata.FileData.Path});
                    }

                    PostUser postuser = await _repoPostUser.FindAll(pu => pu.Email==email && pu.PostId == postid).FirstOrDefaultAsync();
                    postuser.Status = Data.Enum.PostStatus.Seen;
                    notifmodel.Status = postuser.Status;
                    _repoPostUser.Update(postuser);
                    await _unitOfWork.SaveChangeAsync();

                    operationResult = new OperationResult()
                    {
                        Data = notifmodel,
                        Message = "Get data success",
                        StatusCode = StatusCodee.Ok,
                        Success = true
                    };
                }
                else
                {
                    operationResult = new OperationResult()
                    {                      
                        Message = "No record found",
                        StatusCode = StatusCodee.NotFound,
                        Success = true
                    };
                }            
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }

        public async Task<OperationResult> NotiListByUser(string email)
        {
            try
            {
                //get all relevant emails with post
                var items = await _repoPostUser.FindAll(pu => pu.Email == email && pu.Post.Status == Data.Enum.PostStatus.Delivered).Include(pu => pu.Post)
                                    .OrderByDescending(pu=>pu.Post!.CreateDate).ToListAsync();

               
                if (items != null && items.Count>0)
                {
                    List<NotificationViewModel> models = new List<NotificationViewModel>();
                    foreach (var item in items)
                    {
                        var notimodel = _mapper.Map<NotificationViewModel>(item.Post);
                        notimodel.Status = item.Status;
                        models.Add(notimodel);
                    }                                

                    operationResult = new OperationResult()
                    {
                        Data = models,
                        Message = "Get data success",
                        StatusCode = StatusCodee.Ok,
                        Success = true
                    };
                }
                else
                {
                    operationResult = new OperationResult()
                    {
                        Message = "No record found",
                        StatusCode = StatusCodee.NotFound,
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }

        public async Task<Pager> PagingNoti(PostAppPagingDto pagingdto)
        {

            var query = _repoPost.FindAll(s => s.Disable == false).OrderByDescending(p => p.CreateDate);


            var pagingResult = await query.ProjectTo<NotificationViewModel>(_configMapper)
                                        .ToPaginationAsync(pagingdto.Current_Page, pagingdto.Page_Size);

            return pagingResult;
        }
    }
}
