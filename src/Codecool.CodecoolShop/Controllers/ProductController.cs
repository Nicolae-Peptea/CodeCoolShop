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
using Newtonsoft.Json;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public ProductService ProductService { get; set; }
        public CategoryService CategoryService { get; set; }
        public SupplierService SupplierService { get; set; }
        public OrderService OrderService { get; set; }

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                SupplierDaoMemory.GetInstance());
            CategoryService = new CategoryService(ProductCategoryDaoMemory.GetInstance());
            SupplierService = new SupplierService(SupplierDaoMemory.GetInstance());
            OrderService = new OrderService(OrderDaoMemory.GetInstance());
        }

        public IActionResult Index(int category = 1, int supplier = 0)
        {
            ViewBag.Categories = CategoryService.GetCategories();
            ViewBag.Suppliers = SupplierService.GetSuppliers();
            ViewBag.CurrentCategory = category;
            ViewBag.CurrentSupplier = supplier;

            IEnumerable<Product> products = ProductService.GetSortedProducts(category, supplier);

            return View(products.ToList());
        }

        [HttpPost]
        [Route ("api/buy")]
        public string Buy (int id)
        {
            Item item = new Item();
            Product boughtProduct = ProductService.GetProductById(id);
            item.Product = boughtProduct;

            OrderService.BuyProduct(item);
            IEnumerable<Item> orderItems = OrderService.GetAllItems();
            string orderItemsAsJson = JsonConvert.SerializeObject(orderItems); //TODO method to serialize in OrderService

            return orderItemsAsJson;
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
