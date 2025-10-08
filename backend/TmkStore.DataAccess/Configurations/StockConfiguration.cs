using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TmkStore.Core.Models;
using TmkStore.DataAccess.Entities;

namespace TmkStore.DataAccess.Configurations
{
    public class StockConfiguration : IEntityTypeConfiguration<StockEntity>
    {
        public void Configure(EntityTypeBuilder<StockEntity> builder)
        {
            builder.HasKey(x => x.IDStock);

            builder.Property(s => s.City)
                .HasMaxLength(Stock.MAX_CITY_LENGTH)
                .IsRequired();

            builder.Property(s => s.StockName)
                .HasMaxLength(Stock.MAX_STOCK_NAME_LENGTH)
                .IsRequired();

            // Индекс для быстрого поиска по городу
            builder.HasIndex(s => s.City);

            // Индекс для быстрого поиска по названию склада
            builder.HasIndex(s => s.StockName);
        }
    }
}