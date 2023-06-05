using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Entities
{
    [Table("MappingProductWarehouse")]
    public class MappingProductWarehouse : BaseEntity
    {
        public int WareHouseID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }
    }
}