using AutoMapper;
using MyEiu.Application.Const;
using MyEiu.Application.Extensions;
using MyEiu.Automapper.ViewModel.App.FileDatas;
using MyEiu.Data.EF.Interface;
using MyEiu.Data.Entities.App;
using MyEiu.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Application.Services.App.FileDatas
{
    public interface IFileDataService: IBaseService<FileDataViewModel>
    {
        Task<OperationResult> AddMultileFiles(List<FileDataViewModel> models);
    }
    public class FileDataService : BaseService<FileData, FileDataViewModel>, IFileDataService
    {
        private readonly IRepository<FileData> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private OperationResult? operationResult;

        public FileDataService(IRepository<FileData> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper) : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;   

        }

        public async Task<OperationResult> AddMultileFiles(List<FileDataViewModel> models)
        {
            List<FileData> items = _mapper.Map<List<FileData>>(models);
            try
            {
                foreach(FileData item in items)
                {
                    await _repository.AddAsync(item);
                }
                await _unitOfWork.SaveChangeAsync();

                operationResult = new OperationResult()
                {
                    StatusCode = StatusCodee.Ok,
                    Message = MessageReponse.AddSuccess,
                    Success = true,
                    Data = items
                };
            }
            catch(Exception ex)
            {
                operationResult = ex.GetMessageError();
            }
            return operationResult;
        }
    }
}
