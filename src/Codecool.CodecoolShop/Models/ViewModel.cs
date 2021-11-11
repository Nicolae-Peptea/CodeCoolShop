using System;
using System.Collections.Generic;
using DataAccessLayer.Model;

namespace Codecool.CodecoolShop.Models
{
    public class ViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public ViewModel(IEnumerable<Category> categories,
            IEnumerable<Supplier> suppliers,
            IEnumerable<Product> products)
        {
            Categories = categories;
            Suppliers = suppliers;
            Products = products;
        }
    }
}
