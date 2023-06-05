using Microsoft.EntityFrameworkCore;
using ProductManagement.Entities;

namespace ProductManagement
{
    public class ProductManagementContext : DbContext
    {
        public ProductManagementContext(DbContextOptions<ProductManagementContext> options) : base(options)
        {
        }

        #region [DBSet]

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }
        public DbSet<MappingProductWarehouse> MappingProductWarehouses { get; set; }

        #endregion [DBSet]
    }
}