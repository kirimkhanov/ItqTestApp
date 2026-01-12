using ITQTestApp.Application.Contracts.Persistence;

using Microsoft.Extensions.Logging;

namespace ITQTestApp.Infrastructure.Persistence.UnitOfWork
{
    internal sealed class EfUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EfUnitOfWork> _logger;

        public EfUnitOfWork(
            AppDbContext context,
            ILogger<EfUnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task ExecuteAsync(
            Func<CancellationToken, Task> operation,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting transaction.");
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await operation(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                _logger.LogInformation("Transaction committed.");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Transaction rolled back due to error.");
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public void Dispose()
        {
            _logger.LogDebug("Disposing unit of work.");
            _context.Dispose();
        }
    }

}
