using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos
{
    public interface IOrderDao : IDao<OrderItem>
    {
        decimal GetTotalValue();
        int GetTotalQuantity();
    }
}
