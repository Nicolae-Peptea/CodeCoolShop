using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Services
{
    public interface IMailService
    {
        Task Execute(string fromEmail, Dictionary<string, string> receiver);
    }
}
