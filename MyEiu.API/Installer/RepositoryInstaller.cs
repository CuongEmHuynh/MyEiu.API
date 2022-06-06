using MyEiu.API.Installer.Settings;
using MyEiu.Data.EF.Interface;
using MyEiu.Data.EF.Repository;

namespace MyEiu.API.Installer
{
    public class RepositoryInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
        }
    }
}
