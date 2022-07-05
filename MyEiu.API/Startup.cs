using Becamex.Salary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MyEiu.API.Configtion.Middleware;
using MyEiu.Application.Services.App.FileDatas;
using MyEiu.Application.Services.App.Posts;
using MyEiu.Application.Services.App.Users;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<SalaryModule>();

           
            services.AddControllers();
           
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            //Ignore cycle loop if an object has number of children > 32
            services.AddControllers().AddNewtonsoftJson(options =>
                 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
             );
            //add mapper
            //services.AddAutoMapper(typeof(Startup));
            services.AddSingleton(AutoMapperConfig.RegisterMappings().CreateMapper());
            services.AddSingleton(AutoMapperConfig.RegisterMappings());
            //add service
            //builder.Services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISalaryService, SalaryService>();
            services.AddScoped(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFileDataService, FileDataService>();
            services.AddHttpClient();

            //add DBContext
            string EiuDbConnectionStr = Configuration.GetConnectionString("WebEiuDbConnection");
            services.AddDbContext<WebEiuDbContext>(options => options.UseMySql(EiuDbConnectionStr, ServerVersion.AutoDetect(EiuDbConnectionStr)));

            string StaffEiuDbConnectionStr = Configuration.GetConnectionString("StaffEiuDbConnection");
            services.AddDbContext<StaffEiuDbContext>(options => options.UseMySql(StaffEiuDbConnectionStr, ServerVersion.AutoDetect(StaffEiuDbConnectionStr)));
            var connect = Configuration.GetConnectionString("MobileAppDbConnection");
            services.AddDbContext<MobileAppDbContext>(options =>
                                   options.UseSqlServer(
                                       Configuration.GetConnectionString("MobileAppDbConnection")));



        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseMiddleware<ApiKeyMiddleware>();
            }
            else
                app.UseMiddleware<ApiKeyMiddleware>();
            app.UseUnitOfWork();

            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, "wwwroot")),
                RequestPath = "/FileUpload"
            });

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
