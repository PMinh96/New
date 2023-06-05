using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Entities
{
    [Table("WareHouse")] // Bảng lúc nãy nó WareHouseS - Minh thêm cái Attribute Table này có nghĩa là chỉ định trực tiếp tên bảng WareHouse ====> Chữ S của nó đâu?????
    public class WareHouse : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}


/// ----- Trường hợp không có attribute Table kia thì nó có 2 cách để định nghĩa tên bảng:
/// 1: tạo file Configuration
/// 2: nó sẽ ăn theo tên trong DataContext:     public DbSet<WareHouse> WareHouses { get; set; } => Có chữ S