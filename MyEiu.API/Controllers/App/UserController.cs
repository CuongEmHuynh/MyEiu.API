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

        public UserController(IUserService uService)
        {
            _uService = uService;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CheckUserExist(LoginUserDto model) => Ok(await _uService.CheckUserExist(model));
      
    }
}
