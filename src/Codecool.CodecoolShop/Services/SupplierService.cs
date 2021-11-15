using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Services.Interfaces;
using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services
{
    public class SupplierService : ISupplierService
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
