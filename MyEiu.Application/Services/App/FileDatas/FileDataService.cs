using AutoMapper;
using MyEiu.Application.Const;
using MyEiu.Application.Extensions;
using MyEiu.Automapper.ViewModel.App.FileDatas;
using MyEiu.Data.EF.Interface;
using MyEiu.Data.Entities.App;
using MyEiu.Utilities.Dtos;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Net;

namespace MyEiu.Application.Services.App.FileDatas
{
    public interface IFileDataService: IBaseService<FileDataViewModel>
    {
        Task<OperationResult> AddMultileFiles(List<FileDataViewModel> models);
        Task<ActionResult> Download(int id);
    }
    public class FileDataService : BaseService<FileData, FileDataViewModel>, IFileDataService
    {
        private readonly IRepository<FileData> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private OperationResult? operationResult;
        private IHostingEnvironment _environment;

        public FileDataService(IRepository<FileData> repository, IUnitOfWork unitOfWork, IMapper mapper, MapperConfiguration configMapper, IHostingEnvironment environment) 
            : base(repository, unitOfWork, mapper, configMapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configMapper = configMapper;
            _environment = environment;
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

        public async Task<ActionResult> Download(int id)
        {
            var item = _repository.FindById(id);

            string wwwPath = this._environment.WebRootPath;
            var filePath = Path.Combine(wwwPath, item.Path!);
            if (File.Exists(filePath))
            {
                var bytes = await File.ReadAllBytesAsync(filePath);

                //Determine the Content Type of the File.
                string contentType = "";
                new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType!);
               
                return new FileContentResult(bytes, contentType) { FileDownloadName = item.DisplayName };
            }
            else
            {
                return new NotFoundResult();
            }
            
        }
    }
}
