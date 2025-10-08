using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TmkStore.Core.Models;
using TmkStore.DataAccess.Entities;

namespace TmkStore.DataAccess.Configurations
{
    public class PipeTypeConfiguration : IEntityTypeConfiguration<PipeTypeEntity>
    {
        public void Configure(EntityTypeBuilder<PipeTypeEntity> builder)
        {
            builder.HasKey(x => x.IDType);

            builder.Property(pt => pt.Type)
                .HasMaxLength(PipeType.MAX_TYPE_LENGTH)
                .IsRequired();

            // Создаем связь для родительского типа
            builder.HasOne<PipeTypeEntity>()
                .WithMany()
                .HasForeignKey(pt => pt.IDParentType)
                .OnDelete(DeleteBehavior.Restrict);

            // Индекс для быстрого поиска по названию типа
            builder.HasIndex(pt => pt.Type);

            // Индекс для родительского типа для оптимизации запросов иерархии
            builder.HasIndex(pt => pt.IDParentType);
        }
    }
}