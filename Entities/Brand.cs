using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Entities
{
    [Table("Brand")]
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
    }
}