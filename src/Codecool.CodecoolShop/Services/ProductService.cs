using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        private readonly IProductDao productDao;
        private readonly IProductCategoryDao productCategoryDao;
        private readonly ISupplierDao productSupplierDao;

        public ProductService(IProductDao productDao, 
            IProductCategoryDao productCategoryDao,
            ISupplierDao productSupplierDao)
        {
            this.productDao = productDao;
            this.productCategoryDao = productCategoryDao;
            this.productSupplierDao = productSupplierDao;
        }

        public ProductCategory GetProductCategory(int categoryId)
        {
            return this.productCategoryDao.Get(categoryId);
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            ProductCategory category = this.productCategoryDao.Get(categoryId);
            return this.productDao.GetBy(category);
        }

        public IEnumerable<Product> GetProductsForSupplier(int supplierId)
        {
            Supplier supplier = this.productSupplierDao.Get(supplierId);
            return this.productDao.GetBy(supplier);
        }

        public IEnumerable<Product> GetProductsForCategoryAndSupplier(int categoryId, 
            int supplierId)
        {
            ProductCategory category = this.productCategoryDao.Get(categoryId);
            Supplier supplier = this.productSupplierDao.Get(supplierId);
            return this.productDao.GetBy(category, supplier);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return this.productDao.GetAll();
        }


        public Product GetProductById(int id)
        {
           return productDao.Get(id);
        }

        public IEnumerable<Product> GetSortedProducts(int category, int supplier)
        {
            IEnumerable<Product> products;

            if (category != 0 && supplier == 0)
            {
                products = GetProductsForCategory(category);
            }
            else if (category == 0 && supplier != 0)
            {
                products = GetProductsForSupplier(supplier);
            }
            else if (category == 0 && supplier == 0)
            {
                products = GetAllProducts();
            }
            else
            {
                products = GetProductsForCategoryAndSupplier(category, supplier);
            }

            return products;
        }
    }
}
