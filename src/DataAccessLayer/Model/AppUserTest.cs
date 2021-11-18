using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Model
{
    public class AppUserTest : IdentityUser
    {
        public string City { get; set; }

        public int Age { get; set; }
    }
}
