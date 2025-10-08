using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TmkStore.DataAccess.Entities;

namespace TmkStore.DataAccess.Configurations
{
    public class RemnantConfiguration : IEntityTypeConfiguration<RemnantEntity>
    {
        public void Configure(EntityTypeBuilder<RemnantEntity> builder)
        {
            builder.HasKey(x => new { x.ID, x.IDStock });

            builder.Property(r => r.InStockT)
                .HasPrecision(18, 3);

            builder.Property(r => r.InStockM)
                .HasPrecision(18, 3);

            builder.Property(r => r.AvgTubeLength)
                .HasPrecision(18, 3);

            builder.Property(r => r.AvgTubeWeight)
                .HasPrecision(18, 3);

            // Создаем связь с номенклатурой
            builder.HasOne<Entities.NomenclatureEntity>()
                .WithMany()
                .HasForeignKey(r => r.ID)
                .OnDelete(DeleteBehavior.Cascade);

            // Создаем связь со складом
            builder.HasOne<Entities.StockEntity>()
                .WithMany()
                .HasForeignKey(r => r.IDStock)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}