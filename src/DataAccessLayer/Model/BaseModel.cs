using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
