using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Model
{
    public class Product : BaseModel
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int SupplierId { get; set; }

        public int CategoryId { get; set; }

        public Supplier Supplier { get; set; }

        public Category Category { get; set; }
    }
}