using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceAggregator.Domain.Entities;

namespace PriceAggregator.Infrastructure.Repository.Configurations;

public class PriceConfiguration : IEntityTypeConfiguration<PriceAggregate>
{
    public void Configure(EntityTypeBuilder<PriceAggregate> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.OwnsOne(p => p.Instrument)
            .Property(i => i.Symbol)
            .HasColumnName("Instrument");;
    }
}