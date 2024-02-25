using GenericRepository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenericRepository.Infrastructure.Configurations;

public class OutBoxMessageConfiguration : IEntityTypeConfiguration<OutBoxMessage>
{
    public void Configure(EntityTypeBuilder<OutBoxMessage> builder)
    {
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Type)
               .IsRequired()
               .HasMaxLength(150);
        
        builder.Property(b => b.Content)
               .IsRequired();
    }
}