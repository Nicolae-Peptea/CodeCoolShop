using Codecool.CodecoolShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Services
{
    public interface IMailService
    {
        Task Execute(EmailConfirmation model);
    }
}
