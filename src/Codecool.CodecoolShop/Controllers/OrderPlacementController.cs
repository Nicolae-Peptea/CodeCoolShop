using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Stripe;
using System;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Controllers
{
    public class OrderPlacementController : Controller
    {
        public ProductServicesDb ProductService { get; set; }
        public OrderServices OrderServices { get; set; }
        public IMailService EmailService { get; set; }

        public OrderPlacementController(IMailService mailService, CodeCoolShopContext context)
        {
            ProductDaoDb productDao = new(context);
            ProductCategoryDaoDb categoryDao = new(context);
            SupplierDaoDb supplierDao = new(context);
            OrderDaoDb orderDao = new(context);

            ProductService = new ProductServicesDb(productDao, categoryDao, supplierDao);
            OrderServices = new OrderServices(orderDao);
            EmailService = mailService;
        }

        [HttpPost]
        public IActionResult Index(OrderDetails order)
        {
            List<DataAccessLayer.Model.ProductOrder> cartItems = JsonHelper
                .Deserialize<List<DataAccessLayer.Model.ProductOrder>>(order.CartItems);
            IEnumerable<DataAccessLayer.Model.Product> products = ProductService.GetAllProducts();

            List<Models.OrderItem> orderItems = OrderServices
                .GetOrderItems(cartItems, products);

            decimal orderTotal = OrderServices.GetTotalOrderValue(orderItems);

            CustomerService customers = new();
            ChargeService charges = new();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = order.StripeEmail,
                Name = order.StripeBillingName,
            });

            try
            {
                var charge = charges.Create(new ChargeCreateOptions
                {
                    Amount = (long)orderTotal,
                    Description = "Test Payment",
                    Currency = "usd",
                    Source = order.StripeToken,
                });

                Log.Information("Successful checkout process - payment complete");
                EmailConfirmation model = new(order, orderTotal, orderItems);
                EmailService.Execute(model).Wait();
                //OrderServices.EmptyOrder();
                return RedirectToAction("SuccessfulOrder", new { id = 1 });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed the checkout process due to payment");
            }

            return RedirectToAction("Index", "Product");
        }

        public IActionResult SuccessfulOrder(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
