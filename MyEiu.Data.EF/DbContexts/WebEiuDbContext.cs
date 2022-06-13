using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyEiu.Data.Entities.Web;

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
            //var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                //.AddJsonFile($"appsettings.{environmentName}.json")
                .Build();
            var connectionString = configuration.GetConnectionString("WebEiuDbConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostWebEiu>()
               .HasOne(p => p.UserWebEiu)
               .WithMany(u => u.Posts)
               .HasForeignKey(p => p.Post_Author);
            modelBuilder.Entity<ThumbnailWebEiu>()
                .HasOne(t => t.Post)
                .WithMany(p => p.ThumbnailWebEius)
                .HasForeignKey(t => t.post_parent);
            modelBuilder.Entity<TranslationWebEiu>()
                .HasOne(t => t.Post)
                .WithMany(p => p.Translation)
                .HasForeignKey(t => t.Element_Id);

        }
        public DbSet<PostWebEiu>? Posts { get; set; }
        public DbSet<UserWebEiu>? UserWebEius { get; set; }
        public DbSet<ThumbnailWebEiu>? ThumbnailWebEius { get; set; }
        public DbSet<TranslationWebEiu>? Translations{ get; set; }
    }
}
