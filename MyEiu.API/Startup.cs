using Microsoft.EntityFrameworkCore;
using MyEiu.Application.Services.Salary;
using MyEiu.Application.Services.System;
using MyEiu.Automapper.Settings;
using MyEiu.Data.EF.DbContexts;
using MyEiu.Data.EF.Interface;
using MyEiu.Data.EF.Repository;

namespace MyEiu.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
           services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           services.AddEndpointsApiExplorer();
           services.AddSwaggerGen();
            //Ignore cycle loop if an object has number of children > 32
           services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            //add mapper
           services.AddSingleton(AutoMapperConfig.RegisterMappings().CreateMapper());
           services.AddSingleton(AutoMapperConfig.RegisterMappings());
            //add service
            //builder.Services.AddScoped<IUserService, UserService>();
           services.AddScoped<IPayrollService, PayrollService>();
            services.AddScoped(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFileDataService, FileDataService>();




            //add DBContext
            string EiuDbConnectionStr = Configuration.GetConnectionString("WebEiuDbConnection");
           services.AddDbContext<WebEiuDbContext>(options => options.UseMySql(EiuDbConnectionStr, ServerVersion.AutoDetect(EiuDbConnectionStr)));

            string StaffEiuDbConnectionStr = Configuration.GetConnectionString("StaffEiuDbConnection");
           services.AddDbContext<StaffEiuDbContext>(options => options.UseMySql(StaffEiuDbConnectionStr, ServerVersion.AutoDetect(StaffEiuDbConnectionStr)));

           services.AddDbContext<MobileAppDbContext>(options =>
                                   options.UseSqlServer(
                                       Configuration.GetConnectionString("MobileAppDbConnection")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
                app.UseMiddleware<ApiKeyMiddleware>();

            app.UseCors(x => x.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}"
                );
            });
        }
    }
}
