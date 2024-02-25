using GenericRepository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenericRepository.Infrastructure.Configurations;

public class ChangeLogConfiguration : IEntityTypeConfiguration<ChangeHistoryEntity>
{
    public void Configure(EntityTypeBuilder<ChangeHistoryEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.RelatedEntityType)
               .HasMaxLength(150);
    }
}

