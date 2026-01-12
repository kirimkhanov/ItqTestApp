using ITQTestApp.Application.Contracts.Persistence;

using Microsoft.Extensions.Logging;

namespace ITQTestApp.Infrastructure.Persistence.UnitOfWork
{
    internal sealed class EfUnitOfWork : IUnitOfWork
    {
        public IReferenceItemRepository ReferenceItemRepository { get; }
        private readonly AppDbContext _context;
        private readonly ILogger<EfUnitOfWork> _logger;

        public EfUnitOfWork(
            AppDbContext context,
            IReferenceItemRepository referenceItemRepository,
            ILogger<EfUnitOfWork> logger)
        {
            _context = context;
            ReferenceItemRepository = referenceItemRepository;
            _logger = logger;
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Saving changes to the database.");
            var savedCount = await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Changes saved to the database. Rows: {SavedCount}", savedCount);
            return savedCount;
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing unit of work.");
            _context.Dispose();
        }
    }

}
