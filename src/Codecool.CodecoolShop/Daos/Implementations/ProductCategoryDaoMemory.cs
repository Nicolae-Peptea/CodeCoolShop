//using Codecool.CodecoolShop.Models;
//using System.Collections.Generic;

//namespace Codecool.CodecoolShop.Daos.Implementations
//{
//    class ProductCategoryDaoMemory : IProductCategoryDao
//    {
//        private List<ProductCategory> data = new List<ProductCategory>();
//        private static ProductCategoryDaoMemory instance = null;

//        private ProductCategoryDaoMemory()
//        {
//        }

//        public static ProductCategoryDaoMemory GetInstance()
//        {
//            if (instance == null)
//            {
//                instance = new ProductCategoryDaoMemory();
//            }

//            return instance;
//        }

//        public void Add(ProductCategory item)
//        {
//            item.Id = data.Count + 1;
//            data.Add(item);
//        }

//        public void RemoveItem(int id)
//        {
//            data.Remove(this.Get(id));
//        }

//        public ProductCategory Get(int id)
//        {
//            return data.Find(x => x.Id == id);
//        }

//        public IEnumerable<ProductCategory> GetAll()
//        {
//            return data;
//        }
//    }
//}
