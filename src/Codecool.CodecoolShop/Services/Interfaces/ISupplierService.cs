using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services.Interfaces
{
    public interface ISupplierService
    {
        IEnumerable<Supplier> GetSuppliers();
    }
}
