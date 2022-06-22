using MyEiu.Data.Entities.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Data.EF.DbContexts
{
    public class DbInitializer
    {
        private readonly MobileAppDbContext _context;

        public DbInitializer(MobileAppDbContext context)
        {
            _context = context;
        }
        public async Task Seed()
        {
            if(_context.PostTypes!.Any())
            {
                List<PostType> posttypes = new List<PostType>();

                posttypes.Add(new PostType
                {
                    Name = "Thông báo",
                    Description = "Gửi thông báo sự kiện đến người dùng"
                });
                //posttypes.Add(new PostType
                //{
                //    Name = "Tin nhắn",
                //    Description = "Tin nhắn giữa người dùng"
                //});

                _context.PostTypes!.AddRange(posttypes);
                await _context.SaveChangesAsync();
            }
            if (_context.UserRoles!.Any())
            {
                List<UserRole> userroles = new List<UserRole>();

                userroles.Add(new UserRole
                {
                    Name = "Admin",
                    Description = "Người quản trị hệ thống"
                });
                userroles.Add(new UserRole
                {
                    Name = "User",
                    Description = "Người dùng"
                });
                
                _context.UserRoles!.AddRange(userroles);
                await _context.SaveChangesAsync();
            }
            if (_context.Users!.Any())
            {
                List<User> users = new List<User>();

                users.Add(new User
                {
                    Code = "040016",
                    Username = "ngu.nguyen",
                    Password = null,
                    LastName = "Nguyễn",
                    FirstName = "Ngữ",
                    Birthday = new DateTime(1988,9,20),
                    Email = "ngu.nguyen@eiu.edu.vn",
                    RoleId = 2,
                    GroupID=null,
                    Phone = "0977317173",
                    IsDeleted = 0,
                    ImagePath = null
                }) ;

                _context.Users!.AddRange(users);
                await _context.SaveChangesAsync();
            }
        }
    }
}
