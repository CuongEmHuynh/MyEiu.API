
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
               .HasOne(p => p.UserWebEiu)
               .WithMany(u => u.Posts)
               .HasForeignKey(p => p.Post_Author);
            modelBuilder.Entity<ThumbnailWebEiu>()
                .HasOne(t => t.Post)
                .WithMany(p => p.ThumbnailWebEius)
                .HasForeignKey(t => t.post_parent);
            modelBuilder.Entity<Translation>()
                .HasOne(t => t.Post)
                .WithMany(p => p.Translation)
                .HasForeignKey(t => t.Element_Id);

        }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<UserWebEiu>? UserWebEius { get; set; }
        public DbSet<ThumbnailWebEiu>? ThumbnailWebEius { get; set; }
        public DbSet<Translation>? Translations{ get; set; }
    }
}
