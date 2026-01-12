using ITQTestApp.Domain.Entities;
using ITQTestApp.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITQTestApp.Infrastructure.Persistence.Configurations
{
    internal sealed class ReferenceItemConfiguration : IEntityTypeConfiguration<ReferenceItem>
    {
        public void Configure(EntityTypeBuilder<ReferenceItem> builder)
        {
            builder.HasKey(x => x.Code);

            builder.Property(x => x.Code)
                .HasConversion(
                    code => code.Value,
                    value => new Code(value))
                .IsRequired();

            builder.Property(x => x.RowNumber)
                .IsRequired();
        }
    }
}
