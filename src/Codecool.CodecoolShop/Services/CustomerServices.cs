﻿using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Stripe;
using System;
using System.Security.Claims;

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

        public void CreateCustomer(OrderViewDetailsModel order, HttpContext httpContext)
        {
            CustomerService stripeCustomer = new();

            stripeCustomer.Create(new CustomerCreateOptions
            {
                Email = order.StripeEmail,
                Name = order.StripeBillingName,
            });

            DataAccessLayer.Model.Customer customerForDb = MapOrderDetailsToCustomerModel(order);
            DataAccessLayer.Model.Customer alreadyCustomer = _customerDao.GetAlreadyCustomers(order);
            string userId = _userManager.GetUserId(httpContext.User);

            if (userId != null)
            {
                customerForDb.UserId = userId;
            }

            if (alreadyCustomer != null)
            {
                customerForDb.UserId = alreadyCustomer.UserId;
                _customerDao.UpdateCustomer(customerForDb);
            }
            else
            {
                _customerDao.Add(customerForDb);
            }
        }

        private DataAccessLayer.Model.Customer MapOrderDetailsToCustomerModel(OrderViewDetailsModel order)
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

        public string GetUserId(ClaimsPrincipal principal)
        {
            var user = _userManager.GetUserId(principal);
            return user;
        }

        public int GetCustomerId(string userId)
        {
            int customerId = _customerDao.GetId(userId);
            return customerId;
        }

        public DataAccessLayer.Model.Customer Get(int id)
        {
            var customer = _customerDao.Get(id);
            return customer;
        }
    }
}
