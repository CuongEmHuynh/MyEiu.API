using MyEiu.Application.Dtos;
using MyEiu.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Application.System
{
    public interface IAuthService
    {
        Task<OperationResult> LoginAsync(LoginDto login);
        Task<OperationResult> LogoutAsync();

        Task<OperationResult> ResetPasswordAsync(int id);

        Task<OperationResult> ChangePasswordAsync(int id, string password);
    }
    public class AuthService : IAuthService
    {
        public Task<OperationResult> ChangePasswordAsync(int id, string password)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> LoginAsync(LoginDto login)
        {






            throw new NotImplementedException();
        }

        public Task<OperationResult> LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ResetPasswordAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
