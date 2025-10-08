using Microsoft.EntityFrameworkCore;
using TmkStore.DataAccess.Entities;

namespace TmkStore.DataAccess
{
    public class TmkStoreDbContext : DbContext
    {
        public TmkStoreDbContext(DbContextOptions<TmkStoreDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<NomenclatureEntity> Nomenclatures { get; set; }
        public DbSet<RemnantEntity> Remnants { get; set; }
        public DbSet<StockEntity> Stocks { get; set; }
        public DbSet<PriceEntity> Prices { get; set; }
        public DbSet<PipeTypeEntity> PipeTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.ProductConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.NomenclatureConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.RemnantConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.StockConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PriceConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PipeTypeConfiguration());
        }
    }
}
