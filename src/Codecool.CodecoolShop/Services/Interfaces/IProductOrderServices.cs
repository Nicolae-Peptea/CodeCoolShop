using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services.Interfaces
{
    public interface IProductOrderServices
    {
        void AddProducts(List<ProductOrder> productOrders);
        List<ProductOrder> GetAllByOrder(int id);
    }
}
