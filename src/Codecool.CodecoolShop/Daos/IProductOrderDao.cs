using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IProductOrderDao : IDao<ProductOrder>
    {
        IEnumerable<ProductOrder> GetAllByOrder(int id);
    }
}
