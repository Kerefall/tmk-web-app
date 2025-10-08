using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TmkStore.Core.Models;
using TmkStore.DataAccess.Entities;

namespace TmkStore.DataAccess.Configurations
{
    public class NomenclatureConfiguration : IEntityTypeConfiguration<NomenclatureEntity>
    {
        public void Configure(EntityTypeBuilder<NomenclatureEntity> builder)
        {
            builder.HasKey(x => x.ID);

            builder.Property(n => n.Name)
                .HasMaxLength(Nomenclature.MAX_NAME_LENGTH)
                .IsRequired();

            builder.Property(n => n.Gost)
                .HasMaxLength(Nomenclature.MAX_GOST_LENGTH)
                .IsRequired();

            builder.Property(n => n.FormOfLength)
                .HasMaxLength(Nomenclature.MAX_FORM_LENGTH)
                .IsRequired();

            builder.Property(n => n.Manufacturer)
                .HasMaxLength(Nomenclature.MAX_MANUFACTURER_LENGTH)
                .IsRequired();

            builder.Property(n => n.SteelGrade)
                .HasMaxLength(Nomenclature.MAX_STEEL_GRADE_LENGTH)
                .IsRequired();

            builder.Property(n => n.Status)
                .HasMaxLength(Nomenclature.MAX_STATUS_LENGTH)
                .IsRequired();

            builder.Property(n => n.IDTypeNew)
                .IsRequired();

            builder.Property(n => n.ProductionType)
                .IsRequired();

            builder.Property(n => n.IDCat)
                .IsRequired();

            builder.Property(n => n.IDType)
                .IsRequired();
        }
    }
}