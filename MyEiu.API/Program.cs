
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddSingleton(AutoMapperConfig.RegisterMappings().CreateMapper());
builder.Services.AddSingleton(AutoMapperConfig.RegisterMappings());

string EiuDbConnectionStr = builder.Configuration.GetConnectionString("WebEiuDbConnection");
builder.Services.AddDbContext<WebEiuDbContext>(options => options.UseMySql(EiuDbConnectionStr, ServerVersion.AutoDetect(EiuDbConnectionStr)));

string StaffEiuDbConnectionStr = builder.Configuration.GetConnectionString("StaffEiuDbConnection");
builder.Services.AddDbContext<StaffEiuDbContext>(options => options.UseMySql(StaffEiuDbConnectionStr, ServerVersion.AutoDetect(StaffEiuDbConnectionStr)));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

