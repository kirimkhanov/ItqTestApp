using ITQTestApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITQTestApp.Infrastructure.Persistence
{
    public sealed class AppDbContext : DbContext
    {
        public DbSet<ReferenceItem> ReferenceItems => Set<ReferenceItem>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }

}
