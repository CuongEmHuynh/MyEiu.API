using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyEiu.Application.Const;
using MyEiu.Automapper.ViewModel.App;
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
        Task<OperationResult> NewPost(IFormFile file);       
        Task<OperationResult> SuspendedPost(int postid);
        Task<OperationResult> GetPostsByUser(int userid);
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

        public async Task<OperationResult> GetPostsByUser(int userid)
        {
            var item = await _repository.FindAll(p => p.CreateBy == userid).ToListAsync();
            if (item != null)
            {
                operationResult = new OperationResult
                {
                    StatusCode = Const.StatusCodee.Ok,
                    Data = item,
                    Success = true
                };
            }
            else
            {
                operationResult = new OperationResult
                {
                    StatusCode = Const.StatusCodee.Ok,
                    Success = false,
                    Message = "No data"
                };
            }
            return operationResult;
        }

        public Task<OperationResult> NewPost(IFormFile file)
        {
            throw new NotImplementedException();
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
