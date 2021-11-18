using Codecool.CodecoolShop.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Codecool.CodecoolShop.Models
{
    public class SendgridAccountConfirmationModel : SendgridBaseModel
    {
        public string FullName { get; private set; }

        public string Link { get; private set; }

        public SendgridAccountConfirmationModel(IdentityUser user, string link)
        {
            Email = user.Email;
            FullName = UserNameHelper.ExtractUserNameFromEmail(user.Email);
            Link = link;
        }
    }
}
