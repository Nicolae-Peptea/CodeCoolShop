using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
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
        public ProductServicesDb ProductService { get; set; }
        public CategoryService CategoryService { get; set; }
        public SupplierService SupplierService { get; set; }

        public ProductController(CodeCoolShopContext context)
        {
            ProductDaoDb productDao = new(context);
            ProductCategoryDaoDb categoryDao = new(context);
            SupplierDaoDb supplierDao = new(context);

            ProductService = new ProductServicesDb(productDao, categoryDao, supplierDao);
            CategoryService = new CategoryService(categoryDao);
            SupplierService = new SupplierService(supplierDao);
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
