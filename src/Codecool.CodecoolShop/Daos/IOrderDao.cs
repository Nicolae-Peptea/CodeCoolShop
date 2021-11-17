using DataAccessLayer.Model;

namespace Codecool.CodecoolShop.Daos
{
    public interface IOrderDao : IDao<Order>
    {
        Order GetLatestAddedOrder();
    }
}
