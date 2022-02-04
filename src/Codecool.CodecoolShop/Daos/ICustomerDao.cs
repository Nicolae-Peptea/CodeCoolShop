using Codecool.CodecoolShop.Models;
using DataAccessLayer.Model;

namespace Codecool.CodecoolShop.Daos
{
    public interface ICustomerDao : IDao<Customer>
    {
        public int GetCustomerIdByEmail(OrderViewDetailsModel order);

        public Customer GetAlreadyCustomer(string email);

        public void UpdateCustomer(Customer customer, Customer existingCustomer);
        
        int GetId(string userId);

        void CreateOrUpdateCustomer(Customer customer);
    }
}
