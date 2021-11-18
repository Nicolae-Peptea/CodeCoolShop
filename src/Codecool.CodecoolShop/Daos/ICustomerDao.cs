using Codecool.CodecoolShop.Models;
using DataAccessLayer.Model;

namespace Codecool.CodecoolShop.Daos
{
    public interface ICustomerDao : IDao<Customer>
    {
        public int GetCustomerIdByEmail(OrderViewDetails order);

        public bool IsAlreadyCustomer(OrderViewDetails order);

        public void UpdateCustomer(Customer customer);
        int GetId(string userId);
    }
}
