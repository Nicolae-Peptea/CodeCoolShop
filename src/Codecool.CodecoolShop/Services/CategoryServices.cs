using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Services.Interfaces;
using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services
{
    public class CategoryServices : ICategoryService
    {
        private readonly IProductCategoryDao _productCategoryDao;

        public CategoryServices(IProductCategoryDao productCategoryDao)
        {
            _productCategoryDao = productCategoryDao;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _productCategoryDao.GetAll();
        }
    }
}
