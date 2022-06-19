using MyEiu.API.Installer.Settings;
using MyEiu.Application.Services.App.Users;

namespace MyEiu.API.Installer
{
    public class ServiceInstaller : IInstaller
    {
        
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //Add Other services           

            //services.AddScoped<IUserService, UserService>();
        }
    }
}
