using Codecool.CodecoolShop.Models;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Services.Interfaces
{
    public interface IMailService
    {
        Task SendOrderConfirmationEmail(OrderEmailConfirmation model, SendgridSettings sendgridSettings);
    }
}
