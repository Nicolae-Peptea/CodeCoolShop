using Codecool.CodecoolShop.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Codecool.CodecoolShop.ViewModels
{
    public class AccountConfirmationViewModel
    {
        public string FullName { get; private set; }

        public string Link { get; private set; }

        public string Email { get; private set; }

        public AccountConfirmationViewModel(IdentityUser user, string link)
        {
            Email = user.Email;
            FullName = UserNameHelper.ExtractUserNameFromEmail(user.Email);
            Link = link;
        }
    }
}
