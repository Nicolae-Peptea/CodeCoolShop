using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Services
{
    public class CategoryService
    {
        private readonly IProductCategoryDao productCategoryDao;

        public CategoryService(IProductCategoryDao productCategoryDao)
        {
            this.productCategoryDao = productCategoryDao;
        }

        public IEnumerable<ProductCategory> GetCategories()
        {
            return this.productCategoryDao.GetAll();
        }
    }
}
