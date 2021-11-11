using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using Stripe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public ProductServices ProductService { get; set; }
        public CategoryService CategoryService { get; set; }
        public SupplierService SupplierService { get; set; }
        public OrdersServices OrdersServices { get; set; }
        public OrderServices OrderServices { get; set; }
        public IMailService EmailService { get; set; }

        public CartItem MyProperty { get; set; }

        public ProductController(ILogger<ProductController> logger, IMailService mailService, CodeCoolShopContext ctx)
        {
            _logger = logger;
            ProductService = new ProductServices(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                SupplierDaoMemory.GetInstance());
            CategoryService = new CategoryService(ProductCategoryDaoMemory.GetInstance());
            SupplierService = new SupplierService(SupplierDaoMemory.GetInstance());
            OrdersServices = new OrdersServices(OrdersDaoMemory.GetInstance());
            OrderServices = new OrderServices(OrderDaoMemory.GetInstance());
            EmailService = mailService;

        }

        public IActionResult Index(int category = 1, int supplier = 0)
        {
            Log.Information("User is on the main page");
            ViewBag.Categories = CategoryService.GetCategories();
            ViewBag.Suppliers = SupplierService.GetSuppliers();
            ViewBag.CurrentCategory = category;
            ViewBag.CurrentSupplier = supplier;

            IEnumerable<ShopProduct> products = ProductService.GetSortedProducts(category, supplier);

            return View(products.ToList());
        }

        [HttpPost]
        public IActionResult Cart()
        {
            Log.Information("User initialized checkout process");
            decimal result;
            decimal.TryParse(HttpContext.Request.Form["total-value"], out result);
            ViewBag.TotalCart = result * 100;
            return View();
        }

        [HttpPost]
        public IActionResult Charge(OrderDetails order)
        {
            List<CartItem> cartItems = JsonHelper.Deserialize<List<CartItem>>(order.CartItems);
            IEnumerable<ShopProduct> products = ProductService.GetAllProducts();

            List<Models.OrderItem> orderItems = OrderServices.GetOrderItems(cartItems, products).ToList();
            decimal orderTotal = OrderServices.GetTotalOrderValue();

            var customers = new CustomerService();
            var charges = new ChargeService();

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
                EmailConfirmation model = new EmailConfirmation(order, orderTotal, orderItems);
                EmailService.Execute(model).Wait();
                OrderServices.EmptyOrder();
                return RedirectToAction("SuccessfulOrder", new { id = 1 });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed the checkout process due to payment");
            }

            return RedirectToAction("Index");
        }

        public IActionResult SuccessfulOrder(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
