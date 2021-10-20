using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IProductDao : IDao<ShopProduct>
    {
        IEnumerable<ShopProduct> GetBy(Supplier supplier);
        IEnumerable<ShopProduct> GetBy(ProductCategory productCategory);
        IEnumerable<ShopProduct> GetBy(ProductCategory productCategory, Supplier supplier);
    }
}
