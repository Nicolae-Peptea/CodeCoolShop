using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Http;


namespace Codecool.CodecoolShop.Services.Interfaces
{
    public interface ICustomerService
    {
        void CreateCustomer(OrderDetails order, HttpContext httpContext);
    }
}
