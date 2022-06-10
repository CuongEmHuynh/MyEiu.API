using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyEiu.Data.Entities.Staff;

namespace MyEiu.Data.EF.DbContexts
{
    public class StaffEiuDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public StaffEiuDbContext(IConfiguration configuration)
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
            var connectionString = configuration.GetConnectionString("StaffEiuDbConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StaffEiu>()
               .HasOne(s => s.DepartmentEiu)
               .WithMany(d => d.Staffs)
               .HasForeignKey(s => s.DepartmentID);           

        }
        public virtual DbSet<StaffEiu> StaffEius { get; set; }
        public virtual DbSet<DepartmentEiu> Departments { get; set; }
    }
}
