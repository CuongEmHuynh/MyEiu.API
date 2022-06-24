using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyEiu.Application.Const;
using MyEiu.Application.Extensions;
using MyEiu.Automapper.ViewModel.App.Posts;
using MyEiu.Data.EF.Interface;
using MyEiu.Data.Entities.App;
using MyEiu.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Application.Services.App.Posts
{
    public interface IPostService : IBaseService<PostViewModel>
    {
        Task<OperationResult> Add(PostViewModel model);       
        Task<OperationResult> SuspendedPost(int postid);
        Task<List<PostViewModel>> GetPostsByUser(int userid);
        Task<OperationResult> PushNotification(int postid);//push notification to mobile app

    }
    public class PostService : BaseService<Post, PostViewModel>, IPostService
    {
        private readonly IRepository<Post> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private OperationResult? operationResult;

        public PostService(IRepository<Post> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper)
            : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
        }

        public async Task<List<PostViewModel>> GetPostsByUser(int userid)
        {
            var item = await _repository.FindAll(p => p.CreateBy == userid).Include(p => p.Author).Include(p=>p.Editor).ToListAsync();
            return _mapper.Map<List<PostViewModel>>(item);
        }

        public async Task<OperationResult> Add(PostViewModel model)
        {
            var item = _mapper.Map<Post>(model);
            try
            {
                await _repository.AddAsync(item);
                await _unitOfWork.SaveChangeAsync();
                operationResult = new OperationResult
                {
                    StatusCode = StatusCodee.Ok,
                    Message = MessageReponse.AddSuccess,
                    Success = true,
                    Data = item
                };
            }
            catch(Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
           return operationResult;
        }

        public Task<OperationResult> PushNotification(int postid)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> SuspendedPost(int postid)
        {
            throw new NotImplementedException();
        }
    }
}
