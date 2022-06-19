
using Microsoft.EntityFrameworkCore;
using MyEiu.API.Configtion.Middleware;
using MyEiu.Application.Services.App.Users;
using MyEiu.Automapper.Settings;
using MyEiu.Data.EF.DbContexts;
//using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
//builder.Services.InstallServicesInAssembly(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Ignore cycle loop if an object has number of children > 32
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
//add mapper
builder.Services.AddSingleton(AutoMapperConfig.RegisterMappings().CreateMapper());
builder.Services.AddSingleton(AutoMapperConfig.RegisterMappings());
//add service
builder.Services.AddScoped<IUserService, UserService>();
//add DBContext
string EiuDbConnectionStr = builder.Configuration.GetConnectionString("WebEiuDbConnection");
builder.Services.AddDbContext<WebEiuDbContext>(options => options.UseMySql(EiuDbConnectionStr, ServerVersion.AutoDetect(EiuDbConnectionStr)));

string StaffEiuDbConnectionStr = builder.Configuration.GetConnectionString("StaffEiuDbConnection");
builder.Services.AddDbContext<StaffEiuDbContext>(options => options.UseMySql(StaffEiuDbConnectionStr, ServerVersion.AutoDetect(StaffEiuDbConnectionStr)));

builder.Services.AddDbContext<MobileAppDbContext>(options =>
                       options.UseSqlServer(
                           builder.Configuration.GetConnectionString("MobileAppDbConnection")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ApiKeyMiddleware>();
app.UseCors(x => x.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

