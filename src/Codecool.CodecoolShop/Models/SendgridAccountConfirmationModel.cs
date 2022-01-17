using Codecool.CodecoolShop.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Codecool.CodecoolShop.Models
{
    public class SendgridAccountConfirmationModel : SendgridBaseModel
    {
        public string Link { get; private set; }

        public SendgridAccountConfirmationModel(IdentityUser user, string link, string sendgridTemplateId)
        {
            Email = user.Email;
            FullName = UserNameHelper.ExtractUserNameFromEmail(user.Email);
            Link = link;
            TemplateId = sendgridTemplateId;
        }
    }
}
