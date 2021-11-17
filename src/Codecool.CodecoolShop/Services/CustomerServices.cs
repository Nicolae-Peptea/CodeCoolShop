using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Services
{
    public class CustomerServices : ICustomerService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICustomerDao _customerDao;

        public CustomerServices(UserManager<IdentityUser> userManager, ICustomerDao customerDao)
        {
            _userManager = userManager;
            _customerDao = customerDao;
        }

        public void CreateCustomer(OrderViewDetails order, HttpContext httpContext)
        {
            CustomerService stripeCustomer = new();

            stripeCustomer.Create(new CustomerCreateOptions
            {
                Email = order.StripeEmail,
                Name = order.StripeBillingName,
            });

            DataAccessLayer.Model.Customer customerForDb = MapOrderDetailsToCustomerModel(order);
            string userId = _userManager.GetUserId(httpContext.User);

            if (userId != null)
            {
                customerForDb.UserId = userId;
            }

            if (_customerDao.IsAlreadyCustomer(order))
            {
                _customerDao.Add(customerForDb);
            }
            else
            {
                _customerDao.UpdateCustomer(customerForDb);
            }
        }

        private DataAccessLayer.Model.Customer MapOrderDetailsToCustomerModel(OrderViewDetails order)
        {
            return new()
            {
                Email = order.StripeEmail,
                FirstName = order.StripeBillingName,
                BillingName = order.StripeBillingName,
                BillingAddressCountry = order.StripeBillingAddressCountry,
                BillingAddressCity = order.StripeBillingAddressCity,
                BillingAddressLine1 = order.StripeBillingAddressLine1,
                BillingAddressZip = order.StripeBillingAddressZip,
                ShippingAddressCountry = order.StripeBillingAddressCountry,
                ShippingAddressCity = order.StripeShippingAddressCity,
                ShippingAddressLine1 = order.StripeShippingAddressLine1,
                ShippingAddressZip = order.StripeShippingAddressZip,
                ShippingName = order.StripeShippingName,
            };
        }
    }
}
