using ITQTestApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITQTestApp.Infrastructure.Persistence.Configurations
{
    internal sealed class ReferenceItemConfiguration : IEntityTypeConfiguration<ReferenceItem>
    {
        public void Configure(EntityTypeBuilder<ReferenceItem> builder)
        {
            builder.OwnsOne(x => x.Code, owned =>
            {
                owned.Property(p => p.Value)
                     .HasColumnName("code")
                     .IsRequired();
            });
        }
    }
}
