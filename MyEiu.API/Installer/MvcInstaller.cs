using AutoMapper;
using MyEiu.API.Installer.Settings;
using MyEiu.Automapper.Settings;
using MyEiu.Data.EF;
using MyEiu.Data.EF.Interface;
using MyEiu.Data.EF.Repository;

namespace MyEiu.API.Installer
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddTransient<DbInitializer>();

            services.AddScoped<IMapper>(sp =>
            {
                return new Mapper(AutoMapperConfig.RegisterMappings());
            });

            services.AddSingleton(AutoMapperConfig.RegisterMappings());
        }
    }
}
