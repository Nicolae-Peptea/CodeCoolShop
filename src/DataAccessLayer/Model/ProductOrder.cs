using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Model
{
    public class ProductOrder
    {
        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductOrderPrice { get; set; }
    }
}