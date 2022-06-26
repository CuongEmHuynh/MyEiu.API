﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyEiu.API.Dtos;
using MyEiu.Application.Const;
using MyEiu.Application.Extensions;
using MyEiu.Automapper.ViewModel.App.Notification;
using MyEiu.Automapper.ViewModel.App.Posts;
using MyEiu.Data.EF.DbContexts;
using MyEiu.Data.EF.Interface;
using MyEiu.Data.Entities.App;
using MyEiu.Utilities.Dtos;
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
        Task<OperationResult> Add(PostViewModel model);
        Task<OperationResult> Update(PostViewModel model);
        Task<List<PostViewModel>> GetPostsByUser(int userid);

        Task<OperationResult> GetNotiByUser(string email);
        Task<OperationResult> ViewNoti(int postid);//user view notification in details     
        Task<string> PushNoti(int postid);//push notification to mobile app

    }
    public class PostService : BaseService<Post, PostViewModel>, IPostService
    {
        private readonly IRepository<Post> _repository;        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private OperationResult? operationResult;
        private readonly HttpClient _httpClient;
        private readonly StaffEiuDbContext _staffEiuDbContext;
        private readonly MobileAppDbContext _mobileAppDbContext;

        public PostService(IRepository<Post> postRepository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper, HttpClient httpClient,StaffEiuDbContext staffEiuDbContext, MobileAppDbContext mobileAppDbContext)
            : base(postRepository, unitOfWork, mapper, configMapper)
        {
            _repository = postRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
            _httpClient = httpClient;
            _staffEiuDbContext = staffEiuDbContext;
            _mobileAppDbContext = mobileAppDbContext;
        }

        public async Task<List<PostViewModel>> GetPostsByUser(int userid)
        {
            var item = await _repository.FindAll(p => p.CreateBy == userid).Include(p => p.Author).Include(p=>p.Editor).ToListAsync();
            return _mapper.Map<List<PostViewModel>>(item);
        }

        public async Task<OperationResult> Add(PostViewModel model)
        {
            var post = _mapper.Map<Post>(model);           
            try
            {
                await _repository.AddAsync(post);             
                await _unitOfWork.SaveChangeAsync();
                operationResult = new OperationResult
                {
                    StatusCode = StatusCodee.Ok,
                    Message = MessageReponse.AddSuccess,
                    Success = true,
                    Data = post
                };
            }
            catch(Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
           return operationResult;
        }
        public Task<OperationResult> Update(PostViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<string> PushNoti(int postid)
        {
            try
            {
                NotificationDto notif = new NotificationDto();
                //get all relevant emails with post
                Post item = await _mobileAppDbContext.Posts.Where(p => p.Id == postid).Include(p => p.PostGroups).Include(p => p.PostUsers).FirstOrDefaultAsync();
                if (item != null)
                {
                    notif.Type = item.PostTypeId;

                    foreach (var p in item.PostUsers!)
                    {
                        notif.Emails!.Add(p.Email!);
                    }

                    foreach (var p in item.PostGroups!)
                    {
                        var temp =  await _staffEiuDbContext.StaffEius!.Where(d => d.IsDeleted == 0 && d.Type != 4 && d.DepartmentEiu!.RecordID == p.GroupId).Select(s=>s.SchoolEmail).ToListAsync();
                        if (temp!=null)
                        {
                            notif.Emails!.AddRange(temp!);
                        }
                    }

                    //declare data
                    notif.Data!.Title = item.Title;
                    notif.Data.body = item.Description;
                    notif.Data.PostId = item.Id;
                }

                _httpClient.DefaultRequestHeaders.Add("IDCAppApiKey", "IDC@123456");
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://api.becamex.com.vn/eiu/sys/push-notif", notif);

                response.EnsureSuccessStatusCode();
                string apiResponse = await response.Content.ReadAsStringAsync();

                return apiResponse;
              
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
          
        }

        public async Task<OperationResult> ViewNoti(int postid)
        {
            try
            {                
                //get all relevant emails with post
                Post item = await _mobileAppDbContext.Posts.Where(p => p.Id == postid).Include(p => p.Author)
                                                        .Include(p => p.PostFileDatas).ThenInclude(pf=>pf.FileData)
                                                        .FirstOrDefaultAsync();

                if (item != null)
                {
                    NotificationViewModel model = _mapper.Map<NotificationViewModel>(item);
                    foreach(var pfiledata in item.PostFileDatas!)
                    {
                        model.FilesUrl.Add(pfiledata.FileData.Path);
                    }
                   
                    operationResult = new OperationResult()
                    {
                        Data = model,
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

        public Task<OperationResult> GetNotiByUser(string email)
        {
            throw new NotImplementedException();
        }




    }
}
