using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Services.Interfaces
{
    public interface IProductServicesDb
    {
        Category GetProductCategory(int categoryId);
        IEnumerable<Product> GetProductsForCategory(int categoryId);
        IEnumerable<Product> GetProductsForSupplier(int supplierId);
        IEnumerable<Product> GetProductsForCategoryAndSupplier(int categoryId,
            int supplierId);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByTheirId();
        Product GetProductById(int id);
        IEnumerable<Product> GetSortedProducts(int category, int supplier);
    }
}
