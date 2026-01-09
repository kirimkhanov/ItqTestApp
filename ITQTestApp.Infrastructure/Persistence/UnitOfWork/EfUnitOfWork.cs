using ITQTestApp.Application.Contracts.Persistence;

namespace ITQTestApp.Infrastructure.Persistence.UnitOfWork
{
    internal sealed class EfUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public EfUnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task ExecuteAsync(
            Func<CancellationToken, Task> action,
            CancellationToken cancellationToken)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await action(cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }

}
