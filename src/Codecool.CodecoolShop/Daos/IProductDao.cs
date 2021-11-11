using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IProductDao : IDao<Product>   //IDao<ShopProduct>
    {
        //IEnumerable<ShopProduct> GetBy(Supplier supplier);
        //IEnumerable<ShopProduct> GetBy(ProductCategory productCategory);
        //IEnumerable<ShopProduct> GetBy(ProductCategory productCategory, Supplier supplier);

        IEnumerable<Product> GetBy(Supplier supplier);
        IEnumerable<Product> GetBy(Category productCategory);
        IEnumerable<Product> GetBy(Category productCategory, Supplier supplier);
    }
}
