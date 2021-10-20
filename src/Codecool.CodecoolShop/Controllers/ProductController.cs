using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Codecool.CodecoolShop.Helpers;
using Stripe;

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

        public ProductController(ILogger<ProductController> logger)
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
        }

        public IActionResult Index(int category = 1, int supplier = 0)
        {
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
            decimal result;
            decimal.TryParse(HttpContext.Request.Form["total-value"], out result);
            ViewBag.TotalCart = result * 100;
            return View();
        }

        [HttpPost]
        public IActionResult Charge(OrderDetails order)
        {
            List<CartItem> cartItems = JsonHelper.Deserialize <List<CartItem>>(order.CartItems);
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

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = (long)orderTotal,
                Description = "Test Payment",
                Currency = "usd",
                Source = order.StripeToken,
            });

            if (charge.Status == "succeeded")
            {
                string sender = "mihaibuga11@gmail.com";
                EmailConfirmation model = new EmailConfirmation(order);
                model.Total = orderTotal.ToString();
                model.Items = orderItems;
                new MailService().Execute(sender, model).Wait();
            }
            return RedirectToAction("Index");
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
