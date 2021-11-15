using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class AppUserTest : IdentityUser
    {
        public string City { get; set; }

        public int Age { get; set; }
    }
}
