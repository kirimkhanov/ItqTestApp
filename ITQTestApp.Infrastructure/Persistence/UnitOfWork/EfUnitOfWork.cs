using ITQTestApp.Application.Contracts.Persistence;

namespace ITQTestApp.Infrastructure.Persistence.UnitOfWork
{
    internal sealed class EfUnitOfWork : IUnitOfWork
    {
        public IReferenceItemRepository ReferenceItemRepository { get; }
        private readonly AppDbContext _context;

        public EfUnitOfWork(AppDbContext context, IReferenceItemRepository referenceItemRepository)
        {
            _context = context;
            ReferenceItemRepository = referenceItemRepository;
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
