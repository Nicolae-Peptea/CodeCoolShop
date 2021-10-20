using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductServices
    {
        private readonly IProductDao productDao;
        private readonly IProductCategoryDao productCategoryDao;
        private readonly ISupplierDao productSupplierDao;

        public ProductServices(IProductDao productDao, 
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

        public IEnumerable<ShopProduct> GetProductsForCategory(int categoryId)
        {
            ProductCategory category = this.productCategoryDao.Get(categoryId);
            return this.productDao.GetBy(category);
        }

        public IEnumerable<ShopProduct> GetProductsForSupplier(int supplierId)
        {
            Supplier supplier = this.productSupplierDao.Get(supplierId);
            return this.productDao.GetBy(supplier);
        }

        public IEnumerable<ShopProduct> GetProductsForCategoryAndSupplier(int categoryId, 
            int supplierId)
        {
            ProductCategory category = this.productCategoryDao.Get(categoryId);
            Supplier supplier = this.productSupplierDao.Get(supplierId);
            return this.productDao.GetBy(category, supplier);
        }

        public IEnumerable<ShopProduct> GetAllProducts()
        {
            return this.productDao.GetAll();
        }

        public ShopProduct GetProductById(int id)
        {
           return productDao.Get(id);
        }

        public IEnumerable<ShopProduct> GetSortedProducts(int category, int supplier)
        {
            IEnumerable<ShopProduct> products;

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
