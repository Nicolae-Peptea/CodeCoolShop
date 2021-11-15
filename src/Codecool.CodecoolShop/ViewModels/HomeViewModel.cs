using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class HomeViewModel
    {
        public IEnumerable<DataAccessLayer.Model.Category> Categories { get; set; }
        public IEnumerable<DataAccessLayer.Model.Supplier> Suppliers { get; set; }
        public IEnumerable<DataAccessLayer.Model.Product> Products { get; set; }
        public int CurrentCategory { get; set; }
        public int CurrentSupplier { get; set; }

        public HomeViewModel(IEnumerable<DataAccessLayer.Model.Category> categories,
            IEnumerable<DataAccessLayer.Model.Supplier> suppliers,
            IEnumerable<DataAccessLayer.Model.Product> products,
            int currentCategory,
            int currentSupplier)
        {
            Categories = categories;
            Suppliers = suppliers;
            Products = products;
            CurrentCategory = currentCategory;
            CurrentSupplier = currentSupplier;
        }
    }
}
