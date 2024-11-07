using GenericRepository.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenericRepository.Infrastructure.EntitytypeConfigurations;

public class OutBoxMessageConfiguration : IEntityTypeConfiguration<OutBoxMessage>
{
    public void Configure(EntityTypeBuilder<OutBoxMessage> builder)
    {
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.EntityType)
               .IsRequired()
               .HasMaxLength(150);
        
        builder.Property(b => b.Content)
               .IsRequired();
        
        builder.Property(b => b.Error)
                 .HasMaxLength(150);
        
        builder.Property(b => b.OccurredOnUtc)
            .IsRequired()
            .HasColumnType("datetime2")
            .HasPrecision(3);
        
        builder.Property(b => b.ProcessedOnUtc)
            .HasColumnType("datetime2")
            .HasPrecision(3);

        builder.OwnsOne(b => b.EventType, e =>
        {
            e.WithOwner();
        });

        builder.Ignore(b => b.IsProcessed);
    }
}