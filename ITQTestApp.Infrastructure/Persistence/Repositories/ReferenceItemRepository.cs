using ITQTestApp.Application.Common.Pagination;
using ITQTestApp.Application.Contracts.Persistence;
using ITQTestApp.Application.Queries;
using ITQTestApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITQTestApp.Infrastructure.Persistence.Repositories
{
    internal sealed class ReferenceItemRepository : IReferenceItemRepository
    {
        private readonly AppDbContext _context;

        public ReferenceItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task ClearAsync(CancellationToken cancellationToken)
        {
            await _context.ReferenceItems.ExecuteDeleteAsync(cancellationToken);
        }

        public async Task AddRangeAsync(
            IReadOnlyCollection<ReferenceItem> items,
            CancellationToken cancellationToken)
        {
            await _context.ReferenceItems.AddRangeAsync(items, cancellationToken);
        }

        public Task<PagedResult<ReferenceItem>> GetAsync(GetCodeValuesQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
