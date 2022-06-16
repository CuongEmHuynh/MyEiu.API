using Microsoft.AspNetCore.Http;
using MyEiu.Automapper.ViewModel.App;
using MyEiu.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Application.Services.App.Post
{
    public interface IPostService : IBaseService<PostViewModel>
    {
        Task<OperationResult> NewPost(IFormFile file);
        Task<OperationResult> PushNotification(int postid);
        Task<OperationResult> DeleteNotification(int postid);

    }
}
