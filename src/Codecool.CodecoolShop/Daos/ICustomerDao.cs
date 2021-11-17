using Codecool.CodecoolShop.Models;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos
{
    public interface ICustomerDao : IDao<Customer>
    {
        public bool IsAlreadyCustomer(OrderDetails order);

        public void UpdateCustomer(Customer customer);
    }
}
