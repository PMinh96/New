using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Entities
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public int BrandId { get; set; }

        public decimal Price { get; set; }
    }
}