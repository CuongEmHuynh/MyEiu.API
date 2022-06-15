using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MyEiu.Data.Entities.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.EF.DbContexts
{
    public class MobileAppDbContext : DbContext
    {

        public MobileAppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithMany(u => u.PostAuthors)               
                .HasForeignKey(p => p.CreateBy)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Post>()
               .HasOne(p => p.Editor)
               .WithMany(u => u.PostEditors)
               .HasForeignKey(p => p.ModifyBy)
               .OnDelete(DeleteBehavior.Restrict);
           

        }    

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MobileAppDbContext>
        {
            public MobileAppDbContext CreateDbContext(string[] args)
            {              
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")          
                    .Build();
                var builder = new DbContextOptionsBuilder<MobileAppDbContext>();
                var connectionString = configuration.GetConnectionString("MobileAppDbConnection");
                builder.UseSqlServer(connectionString);
                return new MobileAppDbContext(builder.Options);
            }
        }

        public DbSet<Entities.App.File>? File_Apps { get; set; }
        public DbSet<Notification>? Notification_Apps { get; set; }
        public DbSet<Post>? Post_Apps{ get; set; }
        public DbSet<PostFile>? PostFile_Apps { get; set; }
        public DbSet<PostType>? PostType_Apps { get; set; }
        public DbSet<User>? User_Apps { get; set; }
        public DbSet<UserRole>? UserRole_Apps { get; set; }

    }
}
