using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TmkStore.DataAccess.Entities;

namespace TmkStore.DataAccess.Configurations
{
    public class PriceConfiguration : IEntityTypeConfiguration<PriceEntity>
    {
        public void Configure(EntityTypeBuilder<PriceEntity> builder)
        {
            builder.HasKey(x => new { x.ID, x.IDStock });

            builder.Property(p => p.PriceT)
                .HasPrecision(18, 2);

            builder.Property(p => p.PriceLimitT1)
                .HasPrecision(18, 3);

            builder.Property(p => p.PriceT1)
                .HasPrecision(18, 2);

            builder.Property(p => p.PriceLimitT2)
                .HasPrecision(18, 3);

            builder.Property(p => p.PriceT2)
                .HasPrecision(18, 2);

            builder.Property(p => p.PriceM)
                .HasPrecision(18, 2);

            builder.Property(p => p.PriceLimitM1)
                .HasPrecision(18, 3);

            builder.Property(p => p.PriceM1)
                .HasPrecision(18, 2);

            builder.Property(p => p.PriceLimitM2)
                .HasPrecision(18, 3);

            builder.Property(p => p.PriceM2)
                .HasPrecision(18, 2);

            builder.Property(p => p.NDS)
                .HasPrecision(5, 2);

            // Создаем связь с номенклатурой
            builder.HasOne<Entities.NomenclatureEntity>()
                .WithMany()
                .HasForeignKey(p => p.ID)
                .OnDelete(DeleteBehavior.Cascade);

            // Создаем связь со складом
            builder.HasOne<Entities.StockEntity>()
                .WithMany()
                .HasForeignKey(p => p.IDStock)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}