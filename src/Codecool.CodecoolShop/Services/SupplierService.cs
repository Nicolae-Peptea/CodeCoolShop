using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services
{
    public class SupplierService
    {
        private readonly ISupplierDao productSupplierDao;

        public SupplierService(ISupplierDao productSupplierDao)
        {
            this.productSupplierDao = productSupplierDao;
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return this.productSupplierDao.GetAll();
        }
    }
}
