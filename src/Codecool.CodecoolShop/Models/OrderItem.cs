using DataAccessLayer.Model;

namespace Codecool.CodecoolShop.Models
{
    public class OrderItem
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }

        public OrderItem(Product product, int productQuantity)
        {
            this.Product = product;
            this.Quantity = productQuantity;
            this.Total = product.Price * productQuantity;
        }
    }
}
