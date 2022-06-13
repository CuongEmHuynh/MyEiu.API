using MyEiu.Data.EF.DbContexts;

namespace MyEiu.Data.EF
{
    public class DbInitializer
    {
        private readonly AppDbContext _appDbContext;
        private readonly MobileAppDbContext _mobileAppDbcontext;

        public DbInitializer(AppDbContext appDbContext, MobileAppDbContext mobileAppDbcontext)
        {
            _appDbContext = appDbContext;
            _mobileAppDbcontext = mobileAppDbcontext;
        }

        //public async Task Seed()
        //{
        //    if (!_mobileAppDbcontext..Any())
        //    {
        //        await _mobileAppDbcontext.CreateAsync(new AppUser()
        //        {
        //            UserName = "admin",
        //            Name = "Administrator",
        //            Email = "admin@gmail.com",
        //            CreateDate = DateTime.Now,
        //            Status = Status.Active
        //        }, "admin");
        //        var user = await _mobileAppDbcontext.FindByNameAsync("admin");
        //        await _mobileAppDbcontext.AddToRoleAsync(user, "Admin");
        //        await _mobileAppDbcontext.SaveChangesAsync();

        //    }
        //    await _appDbContext.SaveChangesAsync(); 
        //}

       

        //Add Data
    }
}
