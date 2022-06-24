using Microsoft.AspNetCore.Mvc;
using MyEiu.Application.Services.App.FileDatas;
using MyEiu.Automapper.ViewModel.App.FileDatas;
using MyEiu.Utilities.Dtos;

namespace MyEiu.API.Controllers.App
{
    public class FileDataController : APIBaseController
    {
        private readonly IFileDataService _service;

        public FileDataController(IFileDataService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<OperationResult> AddMultipleFile(List<FileDataViewModel> models)
        {
            return await _service.AddMultileFiles(models);
        }
    }
}
