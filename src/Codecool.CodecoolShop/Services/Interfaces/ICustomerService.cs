using Codecool.CodecoolShop.Models;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Codecool.CodecoolShop.Services.Interfaces
{
    public interface ICustomerService
    {
        Customer Get(int id);
        void CreateCustomerFromOrder(OrderViewDetailsModel order, HttpContext httpContext);
        string GetUserId(ClaimsPrincipal principal);
        int GetCustomerId(string userId);
        void CreateOrUpdateCustomerOnEmailConfirmation(string email, string userId);
    }
}
