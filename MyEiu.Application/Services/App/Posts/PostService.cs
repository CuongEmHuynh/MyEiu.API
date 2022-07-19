using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyEiu.API.Dtos;
using MyEiu.Application.Const;
using MyEiu.Application.Dtos;
using MyEiu.Application.Extensions;
using MyEiu.Application.Services.System;
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
        Task<OperationResult> AddPostAsync(PostViewModel model);       
        Task<OperationResult> AddPushAsync(PostViewModel model);
        Task<OperationResult> UpdatePush(PostViewModel model);
        Task<OperationResult> PushNoti(int id);
        Task<OperationResult> RemovePost(int postid);
        Task<OperationResult> UpdatePost(PostViewModel model);
        Task<List<PostViewModel>> GetPostsByUser(int userid);
        Task<PostViewModel> GetPostById(int postid);
        Task<OperationResult> NotiListByUser(NotifPagingDto pagingdto);
        Task<OperationResult> CountNewNotifUser(string email);
        Task<OperationResult> NotiDetails(int postid,string email);//user view notification in details           
        Task<Pager> PagingNoti(PostAppPagingDto pagingdto);

    }
    public class PostService : BaseService<Post, PostViewModel>, IPostService
    {
        private readonly IRepository<Post> _repoPost;
        private readonly IRepository<PostUser> _repoPostUser;
        private readonly IRepository<PostGroup> _repoPostGroup;
        private readonly IRepository<FileData> _repoFileData;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private OperationResult? operationResult;
        private readonly HttpClient _httpClient;
        private readonly StaffEiuDbContext _staffEiuDbContext;

        public PostService(IRepository<Post> postRepository, 
            IRepository<PostUser> repoPostUser, 
            IRepository<FileData> repoFileData,
            IRepository<PostGroup> repoPostGroup,
            IFileService fileService,
            StaffEiuDbContext staffEiuDbContext,
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            MapperConfiguration configMapper, 
            HttpClient httpClient)
            : base(postRepository, unitOfWork, mapper, configMapper)
        {
            _repoPost = postRepository;
            _repoPostUser = repoPostUser;
            _repoFileData = repoFileData;  
            _repoPostGroup = repoPostGroup;
            _fileService = fileService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
            _httpClient = httpClient;
            _staffEiuDbContext = staffEiuDbContext;
        }

        public async Task<List<PostViewModel>> GetPostsByUser(int userid)
        {
            var item = await _repoPost.FindAll(p => p.CreateBy == userid)
                .Include(p => p.Author)
                .Include(p => p.Editor)
                .Include(p => p.PostFileDatas!).ThenInclude(p => p.FileData)
                .OrderByDescending(p => p.CreateDate)
                .ToListAsync();
            return _mapper.Map<List<PostViewModel>>(item);
        }
        public async Task<PostViewModel> GetPostById(int postid)
        {
            var item = await _repoPost.FindAll(p => p.Id == postid)
                .Include(p => p.PostGroups)
                .Include(p => p.PostUsers.Where(pu => pu.GroupId == null))
                .Include(p => p.PostFileDatas!).ThenInclude(p => p.FileData)                
                .FirstOrDefaultAsync();
            if(item == null)
            {
                return null;
            }
            return _mapper.Map<PostViewModel>(item);
        }

        public async Task<OperationResult> AddPostAsync(PostViewModel model)
        {
            model.CreateDate = DateTime.Now;
            var postItem = _mapper.Map<Post>(model);
            try
            {
                //get users from Deparment to add into Post.PostUsers
                if(postItem.PostGroups.Count > 0)
                {
                    foreach(var postGroup in postItem.PostGroups)
                    {
                        var postUserSource = await _staffEiuDbContext.StaffEius.Where(s => s.IsDeleted == 0 && s.Type != 4 && s.DepartmentID == postGroup.GroupId)
                            .Select(s => new { s.DepartmentID, s.SchoolEmail }).ToListAsync();
                        foreach(var pu in postUserSource)
                        {
                            PostUser puTemp = new PostUser() { Email = pu.SchoolEmail, GroupId = pu.DepartmentID, Status = Data.Enum.PostStatus.Draft  };
                            postItem.PostUsers.Add(puTemp);
                        }
                        //
                    }
                    
                    
                }

                await _repoPost.AddAsync(postItem);
                await _unitOfWork.SaveChangeAsync();
                operationResult = new OperationResult()
                {
                    Message = "Lưu thành công",
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

        public async Task<OperationResult> AddPushAsync(PostViewModel model)
        {
            model.CreateDate = DateTime.Now;
            var post = _mapper.Map<Post>(model);
            try
            {
                await _repoPost.AddAsync(post);
                await _unitOfWork.SaveChangeAsync();
                operationResult = new OperationResult()
                {
                    Message = "Check lưu thành công"
                };
                operationResult = await PushNoti(post);               
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }

        public async Task<OperationResult> UpdatePost(PostViewModel model)
        {
            model.ModifyDate = DateTime.Now;
            var post = _mapper.Map<Post>(model);
            try
            {
                //delete all related data (PostGroup, PostUser, PostFileData, FileData)

                //1. Delete Unused PostGroup & PostUser (has GroupId)
                //get list groupdid in PostGroup
                var pgSource = _repoPostGroup.FindAll(pg => pg.PostId == post.Id).ToList();
                List<PostGroup> removePostGroups = new List<PostGroup>();
                foreach (var postGroup in pgSource)
                {
                    var deleteflag = true;
                    foreach (var pg in post.PostGroups!)
                    {
                        if(pg.GroupId == postGroup.GroupId)
                            deleteflag = false;
                    }
                    if (deleteflag)
                    {
                        _repoPostGroup.Remove(postGroup);
                        //delete unused PostUser
                        var pulist = _repoPostUser.FindAll(pu => pu.GroupId == postGroup.GroupId && pu.PostId == post.Id).ToList();
                        _repoPostUser.RemoveMultiple(pulist);
                    }
                }
                //2. Delete unused PostUser (GroupId = null)
                var postUserSource = _repoPostUser.FindAll(pu => pu.PostId == post.Id && pu.GroupId == null).ToList();
                var postUserNew = post.PostUsers!.Where(pu => pu.GroupId == null).ToList();
                foreach (var postUser in postUserSource)
                {
                    var deleteflag = true;
                    foreach (var pu in postUserNew)
                    {                       
                        if (postUser.Email == pu.Email)
                            deleteflag = false;
                    }
                    if (deleteflag)
                    {
                        _repoPostUser.Remove(postUser);
                    }

                }
                //3. Add new PostUser (GroupId not null)
                var newGroupId = post.PostGroups.Where(pg => pg.Id == 0).Select(rs => rs.GroupId).ToList();
                foreach (var groupId in newGroupId)
                {
                    var newPostUser = await _staffEiuDbContext.StaffEius.Where(s => s.IsDeleted == 0 && s.Type != 4 && s.DepartmentID == groupId)
                        .Select(s => new { s.DepartmentID, s.SchoolEmail }).ToListAsync();

                    foreach (var pu in newPostUser)
                    {
                        PostUser puTemp = new PostUser() { Email = pu.SchoolEmail, GroupId = pu.DepartmentID, Status = Data.Enum.PostStatus.Draft };
                        post.PostUsers.Add(puTemp);
                    }
                    //
                }

                //4. Delete unused PostFileData & FileData
                //Later

                _repoPost.Update(post);
                await _unitOfWork.SaveChangeAsync();
                operationResult = new OperationResult()
                {
                    Message = "Cập nhật thành công",
                    Success = true,
                    StatusCode = 200                    
                };               
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }
        public async Task<OperationResult> UpdatePush(PostViewModel model)
        {
            model.CreateDate = DateTime.Now;
            var post = _mapper.Map<Post>(model);
            try
            {
                 _repoPost.Update(post);
                await _unitOfWork.SaveChangeAsync();
                operationResult = new OperationResult()
                {
                    Message = "Check update thành công"
                };
                operationResult = await PushNoti(post);
            }
            catch (Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }
        private async Task<OperationResult> PushNoti(Post item)
        {
            try
            {
                NotificationDto notif = new NotificationDto();
                //get all relevant emails with post
                //Post item = await _mobileAppDbContext.Posts.Where(p => p.Id == postid).Include(p => p.PostGroups).Include(p => p.PostUsers).FirstOrDefaultAsync();                
                if (item != null)
                {
                    if(item.PostTypeId == 1)
                        notif.Type = 0;

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

                //update status post from Draft to delivered
                item!.Status = Data.Enum.PostStatus.Delivered;
                //update createdate of post
                item.CreateDate = DateTime.Now;
                //update status postuser from Draft to NEW
                item.PostUsers!.Select(pu => { pu.Status = Data.Enum.PostStatus.New; return pu; }).ToList();

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
                    if (item.PostTypeId == 1)
                        notif.Type = 0;// type of CĐS

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
                item.CreateDate = DateTime.Now;
                item.PostUsers!.Select(pu => { pu.Status = Data.Enum.PostStatus.New; return pu; }).ToList();

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
        public async Task<OperationResult> RemovePost(int postid)
        {
            try
            {               
                var post = await _repoPost.FindAll(p => p.Id==postid).Include(p => p.PostFileDatas).ThenInclude(pfd=>pfd.FileData)
                                                            .Include(p => p.PostGroups).Include(p => p.PostUsers).FirstOrDefaultAsync();
                if(post != null)
                {
                    if(post.PostFileDatas!=null)
                    {
                        foreach (var postfileData in post.PostFileDatas!)
                        {
                            _fileService.RemoveFilePost(postfileData.FileData!.FileName!);
                            _repoFileData.Remove(postfileData.FileData);
                        }
                    }                   

                    _repoPost.Remove(post);
                    await _unitOfWork.SaveChangeAsync();
                }
                              
                operationResult = new OperationResult()
                {
                    Message = "Remove complete",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch(Exception ex)
            {
                operationResult = ex.GetMessageError();
            }

            return operationResult;

        }       
        public async Task<OperationResult> CountNewNotifUser(string email)
        {
            try
            {
                //get all relevant emails with post
                var num = await _repoPostUser.FindAll(pu => pu.Email == email && pu.Status == Data.Enum.PostStatus.New)
                            .CountAsync();


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
                        notifmodel.FilesUrl.Add(new NotifFile { 
                            Name = pfiledata.FileData!.DisplayName, 
                            Path = "/" + pfiledata.FileData.Path,
                            FileDataId = pfiledata.FileDataId
                        });
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

        public async Task<OperationResult> NotiListByUser(NotifPagingDto pagingdto)
        {
            try
            {
                //get all relevant emails with post
                var query =  _repoPostUser.FindAll(pu => pu.Email == pagingdto.Email && pu.Post!.Status == Data.Enum.PostStatus.Delivered).Include(pu => pu.Post!.Author)
                                    .OrderByDescending(pu => pu.Post!.CreateDate);


                var pagingResult = await query.ProjectTo<NotificationViewModel>(_configMapper)
                                       .ToPaginationAsync(pagingdto.Current_Page, pagingdto.Page_Size);

                operationResult = new OperationResult()
                {
                    Data = pagingResult,
                    Message = "Get data success",
                    StatusCode = StatusCodee.Ok,
                    Success = true
                };
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
