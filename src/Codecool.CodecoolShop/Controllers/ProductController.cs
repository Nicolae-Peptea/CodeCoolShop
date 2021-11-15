using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Codecool.CodecoolShop.Services.Interfaces;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Diagnostics;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        public IProductServicesDb ProductService { get; set; }
        public ICategoryService CategoryService { get; set; }
        public ISupplierService SupplierService { get; set; }

        public ProductController(IProductServicesDb productServices,
            ICategoryService categoryService, ISupplierService supplierService)
        {
            ProductService = productServices;
            CategoryService = categoryService;
            SupplierService = supplierService;
        }

        public IActionResult Index(int category = 0, int supplier = 0)
        {
            Log.Information("User is on the main page");

            IEnumerable<DataAccessLayer.Model.Category> categories = CategoryService.GetCategories();
            IEnumerable<DataAccessLayer.Model.Supplier> suppliers = SupplierService.GetSuppliers();
            IEnumerable<DataAccessLayer.Model.Product> products = ProductService.GetSortedProducts(category, supplier);

            ViewModel viewModel = new(categories, suppliers, products, category, supplier);

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
