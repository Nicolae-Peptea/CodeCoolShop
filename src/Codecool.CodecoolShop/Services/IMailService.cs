using Codecool.CodecoolShop.Models;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Services
{
    public interface IMailService
    {
        Task SendEmail(EmailConfirmation model);
    }
}
