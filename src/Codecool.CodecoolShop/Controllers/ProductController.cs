using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
//using DataAccessLayer.Model;
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
        public ProductServicesDb ProductService { get; set; }
        public CategoryService CategoryService { get; set; }
        public SupplierService SupplierService { get; set; }
        public OrderServices OrderServices { get; set; }
        public IMailService EmailService { get; set; }

        public ProductController(ILogger<ProductController> logger, 
            IMailService mailService, CodeCoolShopContext context)
        {
            _logger = logger;
            ProductDaoDb productDao = new(context);
            ProductCategoryDaoDb categoryDao = new(context);
            SupplierDaoDb supplierDao = new(context);
            OrderDaoDb orderDao = new(context);

            ProductService = new ProductServicesDb(productDao, categoryDao, supplierDao);
            CategoryService = new CategoryService(categoryDao);
            SupplierService = new SupplierService(supplierDao);
            OrderServices = new OrderServices(orderDao);
            EmailService = mailService;
        }

        public IActionResult Index(int category = 1, int supplier = 0)
        {
            Log.Information("User is on the main page");

            IEnumerable<DataAccessLayer.Model.Category> categories = CategoryService.GetCategories();
            IEnumerable<DataAccessLayer.Model.Supplier> suppliers = SupplierService.GetSuppliers();
            IEnumerable<DataAccessLayer.Model.Product> products = ProductService.GetSortedProducts(category, supplier);

            ViewModel viewModel = new(categories, suppliers, products, category, supplier);

            return View(viewModel);
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
