using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}