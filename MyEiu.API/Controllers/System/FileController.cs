using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyEiu.Application.Services.System;
using System.IO;

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
        public async Task<IActionResult> UploadMultiFiles([FromForm] List<IFormFile> files, int userid)
        {
            return Ok( await _service.UploadMultiFiles(files, userid));            
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, int userid)
        {
            return Ok( await _service.UploadFile(file, userid));
        }


    }
}
