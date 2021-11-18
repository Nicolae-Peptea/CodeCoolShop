using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Diagnostics;

namespace Codecool.CodecoolShop.Controllers
{
    public class HomePageController : Controller
    {
        private readonly IProductServicesDb _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;

        public HomePageController(IProductServicesDb productServices,
            ICategoryService categoryService, ISupplierService supplierService)
        {
            _productService = productServices;
            _categoryService = categoryService;
            _supplierService = supplierService;
        }

        public IActionResult Index(int category = 0, int supplier = 0)
        {
            Log.Information("User is on the main page");

            IEnumerable<DataAccessLayer.Model.Category> categories = _categoryService.GetCategories();
            IEnumerable<DataAccessLayer.Model.Supplier> suppliers = _supplierService.GetSuppliers();
            IEnumerable<DataAccessLayer.Model.Product> products = _productService.GetSortedProducts(category, supplier);

            HomeViewModel viewModel = new(categories, suppliers, products, category, supplier);

            return View(viewModel);
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
