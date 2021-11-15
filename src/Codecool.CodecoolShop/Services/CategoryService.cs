using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Services.Interfaces;
using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IProductCategoryDao productCategoryDao;

        public CategoryService(IProductCategoryDao productCategoryDao)
        {
            this.productCategoryDao = productCategoryDao;
        }

        public IEnumerable<Category> GetCategories()
        {
            return this.productCategoryDao.GetAll();
        }
    }
}
