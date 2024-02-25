using GenericRepository.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenericRepository.Infrastructure.EntitytypeConfigurations;

public class ChangeLogConfiguration : IEntityTypeConfiguration<ChangeHistoryEntity>
{
    public void Configure(EntityTypeBuilder<ChangeHistoryEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.RelatedEntityType)
               .HasMaxLength(150);
    }
}

