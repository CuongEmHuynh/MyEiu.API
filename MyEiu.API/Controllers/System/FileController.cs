using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyEiu.Application.Services.System;
using MyEiu.Utilities.Dtos;

namespace MyEiu.API.Controllers.System
{
    [AllowAnonymous]
    public class FileController : APIBaseController
    {
        private readonly IFileService _service;

        public FileController(IFileService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult RemoveFilePost(string fileName)
        {
            return Ok(_service.RemoveFilePost(fileName));
        }

        [HttpPost]
        public async Task<OperationFileResult> UploadMultiFiles([FromForm] List<IFormFile> files, int userid)
        {
            OperationFileResult rs = await _service.UploadMultiFiles(files, userid);

            return rs;
        }

        [HttpPost]
        public async Task<OperationFileResult> UploadFile([FromForm] IFormFile file, int userid,string folderpath)
        {
            OperationFileResult rs = await _service.UploadFile(file, userid,folderpath);

            return rs;
        }
    }
}
