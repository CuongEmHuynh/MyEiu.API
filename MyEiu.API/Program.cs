
using Microsoft.EntityFrameworkCore;
using MyEiu.API.Configtion.Middleware;
using MyEiu.Application.Services.App.Posts;
using MyEiu.Application.Services.App.Users;
using MyEiu.Application.Services.System;
using MyEiu.Automapper.Settings;
using MyEiu.Data.EF.DbContexts;
//using System.Text.Json.Serialization;

using MyEiu.API;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });


}
