using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Services.Interfaces;
using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductDao _productDao;
        private readonly IProductCategoryDao _productCategoryDao;
        private readonly ISupplierDao _productSupplierDao;

        public ProductServices(IProductDao productDao,
            IProductCategoryDao productCategoryDao,
            ISupplierDao productSupplierDao)
        {
            _productDao = productDao;
            _productCategoryDao = productCategoryDao;
            _productSupplierDao = productSupplierDao;
        }

        public Category GetProductCategory(int categoryId)
        {
            return _productCategoryDao.Get(categoryId);
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            Category category = _productCategoryDao.Get(categoryId);
            return _productDao.GetBy(category);
        }

        public IEnumerable<Product> GetProductsForSupplier(int supplierId)
        {
            Supplier supplier = _productSupplierDao.Get(supplierId);
            return _productDao.GetBy(supplier);
        }

        public IEnumerable<Product> GetProductsForCategoryAndSupplier(int categoryId,
            int supplierId)
        {
            Category category = _productCategoryDao.Get(categoryId);
            Supplier supplier = _productSupplierDao.Get(supplierId);
            return _productDao.GetBy(category, supplier);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productDao.GetAll();
        }

        public IEnumerable<Product> GetProductsByTheirId()
        {
            return _productDao.GetAll();
        }

        public Product GetProductById(int id)
        {
            return _productDao.Get(id);
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
