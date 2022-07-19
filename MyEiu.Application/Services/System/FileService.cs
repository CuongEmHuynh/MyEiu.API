using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MyEiu.Application.Const;
using MyEiu.Application.Extensions;
using MyEiu.Ultilities;
using MyEiu.Utilities.Dtos;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEiu.Data.EF.Interface;
using MyEiu.Data.Entities.App;
using Microsoft.EntityFrameworkCore;

namespace MyEiu.Application.Services.System
{
    public interface IFileService
    {
        Task<OperationFileResult> UploadFile(IFormFile file, int userid);
        Task<OperationFileResult> UploadMultiFiles(List<IFormFile> files, int userid);
        OperationResult RemoveFilePost(string filename);
        Task<OperationResult> RemoveFileAndData(int filedataid);
    }
    public class FileService : IFileService
    {
        private IHostingEnvironment _env;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<FileData> _repoFileData;
        private readonly IRepository<PostFileData> _repoPostFileData;

        public FileService(IHostingEnvironment env, IUnitOfWork unitOfWork, IRepository<PostFileData> repoPostFileData, IRepository<FileData> repoFileData)
        {
            _env = env;
            _unitOfWork = unitOfWork;
            _repoPostFileData = repoPostFileData;
            _repoFileData = repoFileData;
        }

        public OperationResult RemoveFilePost(string filename)
        {
            string folderPath = "wwwroot/FileUpload/Post";
            string filePath = Path.Combine(folderPath, filename);

            OperationResult operationResult = new OperationResult();
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    operationResult = new OperationResult
                    {
                        Success = true,
                        StatusCode = StatusCodee.Ok,
                        Message = MessageReponse.DeleteSuccess,
                        Data = filename
                    };
                }
                catch (Exception ex)
                {
                    operationResult = ex.GetMessageError();
                }

            }
            else
            {
                operationResult = new OperationResult
                {
                    Success = false,
                    StatusCode = StatusCodee.Ok,
                    Message = MessageReponse.DeleteError,
                    Data = filename
                };

            }
            return operationResult;
        }
        public async Task<OperationResult> RemoveFileAndData(int filedataid)
        {
            //var fileData = _repoFileData.FindById(filedataid);
            var fileData = await _repoFileData.FindAll(f => f.Id == filedataid).Include(f => f.PostFileData).FirstOrDefaultAsync();
            if (fileData != null)
            {
                //delete FileData & PostFileData
                _repoFileData.Remove(fileData);
                _repoPostFileData.Remove(fileData.PostFileData!);

                await _unitOfWork.SaveChangeAsync();


                string folderPath = "wwwroot/FileUpload/Post";
                string filePath = Path.Combine(folderPath, fileData.FileName!);

                OperationResult operationResult = new OperationResult();
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);

                        
                        operationResult = new OperationResult
                        {
                            Success = true,
                            StatusCode = StatusCodee.Ok,
                            Message = MessageReponse.DeleteSuccess,
                            Data = fileData.DisplayName!
                        };
                    }
                    catch (Exception ex)
                    {
                        operationResult = ex.GetMessageError();
                    }

                }               
                return new OperationResult
                {
                    Success = true,
                    StatusCode = StatusCodee.Ok,
                    Message = "File data was deleted",
                    Data = fileData.FileName!
                };
            }
            
            return new OperationResult
            {
                Success = false,
                StatusCode = StatusCodee.Ok,
                Message = "File not found in Database",
                Data = fileData.FileName!
            };
        }
        public async Task<OperationFileResult> UploadFile(IFormFile file, int userid)
        {
            string folderPath = "FileUpload/Post/";
            string folderRoot = _env.WebRootPath;
            bool exists = Directory.Exists(Path.Combine(folderRoot, folderPath));
            if (!exists)
                Directory.CreateDirectory(Path.Combine(folderRoot, folderPath));
            var nowDate = DateTime.Now;
            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = Path.GetFileNameWithoutExtension(file.FileName).ToFileFormat();

            //userid = userid.ToCompactAllSpaces().ToNoSignFormat(true);
            string fileNewName = userid + "_" + nowDate.Day + "_" + nowDate.Month + "_" + nowDate.Year + nowDate.Ticks + fileExtension.ToLower();

            var operationFileResult = new OperationFileResult();
            try
            {
                using (FileStream fs = File.Create("wwwroot/" + folderPath + fileNewName))
                {
                    await file.CopyToAsync(fs);
                    fs.Flush();
                    var fileResponse = new FileResponse
                    {
                        FileLocalName = fileNewName,
                        FileOriginalName = file.FileName,
                        FileExtension = fileExtension,
                        FileType = file.ContentType,
                        FileFullPath = folderPath + fileNewName
                    };

                    operationFileResult = new OperationFileResult() { Success = true, FileResponse = fileResponse };
                }
            }
            catch (Exception ex)
            {
                operationFileResult = new OperationFileResult()
                {
                    Message = ex.ToString(),
                    Success = false
                };
            }
            return operationFileResult;
        }
        public async Task<OperationFileResult> UploadMultiFiles(List<IFormFile> files, int userid)
        {
            var listFileResponse = new List<FileResponse>();
            foreach (var file in files)
            {
                var operationResult = await UploadFile(file, userid);
                if (operationResult.Success)
                {
                    listFileResponse.Add(operationResult.FileResponse);
                }
            }
            return new OperationFileResult() { Success = true, FileResponses = listFileResponse };
        }
     
    }
}
