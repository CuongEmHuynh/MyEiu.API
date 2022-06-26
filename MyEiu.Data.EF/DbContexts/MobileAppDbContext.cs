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
        protected override async void OnModelCreating(ModelBuilder builder)
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

            await new DbInitializer(builder).Seed();
           

        }    

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MobileAppDbContext>
        {
            public MobileAppDbContext CreateDbContext(string[] args)
            {              
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(@"appsettings.json")          
                    .Build();
                var builder = new DbContextOptionsBuilder<MobileAppDbContext>();
                var connectionString = configuration.GetConnectionString("MobileAppDbConnection");
                builder.UseSqlServer(connectionString);
                return new MobileAppDbContext(builder.Options);
            }
        }

        public DbSet<FileData>? FileDatas { get; set; }      
        public DbSet<PostGroup>? PostGroups { get; set; }
        public DbSet<PostUser>? PostUsers { get; set; }
        public DbSet<Post>? Posts{ get; set; }
        public DbSet<PostFileData>? PostFileDatas { get; set; }
        public DbSet<PostType>? PostTypes { get; set; }
        public DbSet<UserApp>? UserApps { get; set; }
        public DbSet<UserRole>? UserRoles { get; set; }

    }
}
