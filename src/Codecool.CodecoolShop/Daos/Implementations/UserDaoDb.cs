using DataAccessLayer.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class UserDaoDb : IUsersDao
    {
        private readonly CodeCoolShopContext _context;

        public UserDaoDb(CodeCoolShopContext context)
        {
            _context = context;
        }
        public IdentityUser Get(string email)
        {
            return _context.Users.Find(email);
        }
    }
}
