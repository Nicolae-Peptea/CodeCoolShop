using Codecool.CodecoolShop.Models;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Codecool.CodecoolShop.Services.Interfaces
{
    public interface ICustomerService
    {
        Customer Get(int id);
        void CreateCustomer(OrderViewDetailsModel order, HttpContext httpContext);
        string GetUserId(ClaimsPrincipal principal);
        int GetCustomerId(string userId);
        void UpdateOrCreateCustomer(string email, string userId);
    }
}
