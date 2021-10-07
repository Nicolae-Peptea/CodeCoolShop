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
using Codecool.CodecoolShop.Helpers;

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

        public IActionResult Cart()
        {
            return View();
        }

        [HttpPost]
        [Route ("api/add-cart-item")]
        public string AddItemToCart (int id, int quantity)
        {
            Product boughtProduct = ProductService.GetProductById(id);
            Item item = ItemHelper.GetItem(boughtProduct);

            OrderService.BuyProduct(item, quantity);
            string orderItemsAsJson = OrderService.GetItemsAsJson();

            return orderItemsAsJson;
        }

        //[FromBody] int id, [FromBody] string name

        [HttpDelete]
        [Route("api/remove-cart-item")]
        public string RemoveCartItem(int id)
        {
            OrderService.RemoveItem(id);

            string orderItemsAsJson = OrderService.GetItemsAsJson();

            return orderItemsAsJson;
        }

        [HttpPut]
        [Route("api/decrease-item-qunatity")]
        public string EditCartItems(int id, int quantity)
        {
            Product boughtProduct = ProductService.GetProductById(id);
            Item item = ItemHelper.GetItem(boughtProduct);

            OrderService.DecreaseItemQuantity(item, quantity);

            string orderItemsAsJson = OrderService.GetItemsAsJson();

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
