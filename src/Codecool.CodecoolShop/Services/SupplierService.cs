using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Services.Interfaces;
using DataAccessLayer.Model;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierDao _productSupplierDao;

        public SupplierService(ISupplierDao productSupplierDao)
        {
            _productSupplierDao = productSupplierDao;
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return _productSupplierDao.GetAll();
        }
    }
}
