using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEiu.Application.Dtos.User;
using MyEiu.Application.Services.App.Users;
using MyEiu.Data.EF.DbContexts;
using MyEiu.Utilities.Dtos;

namespace MyEiu.API.Controllers.App
{
    public class UserController : APIBaseController
    {
        private readonly IUserService _uService;
        private readonly MobileAppDbContext _context;

        public UserController(MobileAppDbContext context, IUserService uService)
        {
            _context = context;
            _uService = uService;   
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CheckUserExist(LoginUserDto model) => Ok(await _uService.CheckUserExist(model));
        //public async Task<ActionResult> CheckUserExist(LoginUserDto model)
        //{
        //    var result = await _context.Users!.Where(u => u.Username == model.Username && u.Email == model.Email).FirstOrDefaultAsync();
        //    if (result != null)
        //    {
        //        operationResult = new OperationResult
        //        {
        //            StatusCode = 200,
        //            Data = result,
        //            Success = true
        //        };
        //    }
        //    else
        //    {
        //        operationResult = new OperationResult
        //        {
        //            StatusCode = 200,
        //            Success = false,
        //            Message = "Người dùng không được tìm thấy"
        //        };
        //    }
        //    return Ok(operationResult);
        //}

    }
}
