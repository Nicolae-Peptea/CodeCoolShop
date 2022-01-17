using Codecool.CodecoolShop.Models;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmail(SendgridBaseModel model);
    }
}
