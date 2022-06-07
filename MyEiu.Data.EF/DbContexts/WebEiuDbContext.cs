
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyEiu.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.EF.DbContexts
{
    public class WebEiuDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public WebEiuDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json")
                .Build();
            var connectionString = configuration.GetConnectionString("WebEiuDbConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Post>? Posts { get; set; }
        public DbSet<UserWebEiu>? UserWebEius { get; set; }
        public DbSet<ThumbnailWebEiu>? ThumbnailWebEius { get; set; }
    }
}
