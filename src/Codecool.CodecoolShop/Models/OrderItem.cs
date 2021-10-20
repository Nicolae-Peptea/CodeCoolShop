namespace Codecool.CodecoolShop.Models
{
    public class OrderItem
    {
        public ShopProduct Product { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }

        public OrderItem(ShopProduct product, int productQuantity)
        {
            this.Product = product;
            this.Quantity = productQuantity;
            this.Total = product.DefaultPrice * productQuantity;
        }
    }
}
