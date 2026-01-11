using ITQTestApp.Application.Common.Pagination;
using ITQTestApp.Application.Contracts.Persistence;
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

        public Task<PagedResult<ReferenceItem>> GetAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            return GetPagedAsync(page, pageSize, cancellationToken);
        }

        private async Task<PagedResult<ReferenceItem>> GetPagedAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var totalCount = await _context.ReferenceItems.CountAsync(cancellationToken);

            var skip = (page - 1) * pageSize;

            var items = await _context.ReferenceItems
                .AsNoTracking()
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<ReferenceItem>(items, totalCount);
        }
    }
}
