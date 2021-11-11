using Codecool.CodecoolShop.Daos;
using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services
{
    public class ProductServicesDb
    {
        private readonly IProductDao productDao;
        private readonly IProductCategoryDao productCategoryDao;
        private readonly ISupplierDao productSupplierDao;

        public ProductServicesDb(IProductDao productDao,
            IProductCategoryDao productCategoryDao,
            ISupplierDao productSupplierDao)
        {
            this.productDao = productDao;
            this.productCategoryDao = productCategoryDao;
            this.productSupplierDao = productSupplierDao;
        }

        public Category GetProductCategory(int categoryId)
        {
            return this.productCategoryDao.Get(categoryId);
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            Category category = this.productCategoryDao.Get(categoryId);
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
            Category category = this.productCategoryDao.Get(categoryId);
            Supplier supplier = this.productSupplierDao.Get(supplierId);
            return this.productDao.GetBy(category, supplier);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return this.productDao.GetAll();
        }

        public IEnumerable<Product> GetProductsByTheirId()
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
