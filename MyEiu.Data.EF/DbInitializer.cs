using MyEiu.Data.EF.DbContexts;

namespace MyEiu.Data.EF
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;

        public DbInitializer(AppDbContext context)
        {
            _context = context;
        }

        //Add Data
    }
}
