
using Microsoft.EntityFrameworkCore;
using MyEiu.API.Installer.Settings;
using MyEiu.Automapper.Settings;
using MyEiu.Data.EF.DbContexts;
using MyEiu.Data.EF.Interface;
using MyEiu.Data.EF.Repository;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
//builder.Services.InstallServicesInAssembly(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton(AutoMapperConfig.RegisterMappings().CreateMapper());
builder.Services.AddSingleton(AutoMapperConfig.RegisterMappings());

string EiuDbConnectionStr = builder.Configuration.GetConnectionString("WebEiuDbConnection");
builder.Services.AddDbContext<WebEiuDbContext>(options => options.UseMySql(EiuDbConnectionStr, ServerVersion.AutoDetect(EiuDbConnectionStr)));


var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

