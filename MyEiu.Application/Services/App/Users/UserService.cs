using AutoMapper;
using MyEiu.Application.Dtos.User;
using MyEiu.Automapper.ViewModel.App.User;
using MyEiu.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEiu.Data.Entities.App;
using MyEiu.Data.EF.Interface;
using Microsoft.EntityFrameworkCore;
using MyEiu.Application.Const;

namespace MyEiu.Application.Services.App.Users
{
    public interface IUserService : IBaseService<UserViewModel>
    {
        Task<OperationResult> CheckUserExist(LoginUserDto model);
    }
    public class UserService : BaseService<UserApp,UserViewModel>, IUserService
    {
        private readonly IRepository<UserApp> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private OperationResult? operationResult;

        public UserService(IRepository<UserApp> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper) 
            : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
        }

        public async Task<OperationResult> CheckUserExist(LoginUserDto model)
        {
            var item = await _repository.FindAll(u => u.Email == model.Email && u.Username == model.Username).FirstOrDefaultAsync();
            if(item != null)
            {
                operationResult = new OperationResult
                {
                    StatusCode = StatusCodee.Ok,
                    Data = item,
                    Success = true
                };
            }
            else
            {
                operationResult = new OperationResult
                {
                    StatusCode = StatusCodee.Ok,
                    Success = false,
                    Message = "Người dùng không được tìm thấy"
                };
            }
            return operationResult;
        }
    }
}
