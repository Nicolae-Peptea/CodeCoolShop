using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Services.Interfaces;
using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services
{
    public class CategoryServices : ICategoryService
    {
        private readonly IProductCategoryDao productCategoryDao;

        public CategoryServices(IProductCategoryDao productCategoryDao)
        {
            this.productCategoryDao = productCategoryDao;
        }

        public IEnumerable<Category> GetCategories()
        {
            return this.productCategoryDao.GetAll();
        }
    }
}
