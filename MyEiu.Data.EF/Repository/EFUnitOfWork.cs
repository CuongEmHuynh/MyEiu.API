using MyEiu.Data.EF.DbContexts;
using MyEiu.Data.EF.Interface;

namespace MyEiu.Data.EF.Repository
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly MobileAppDbContext _context;

        public EFUnitOfWork(MobileAppDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
