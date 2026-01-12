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
            string? search,
            CancellationToken cancellationToken)
        {
            return GetPagedAsync(page, pageSize, search, cancellationToken);
        }

        private async Task<PagedResult<ReferenceItem>> GetPagedAsync(
            int page,
            int pageSize,
            string? search,
            CancellationToken cancellationToken)
        {
            var query = _context.ReferenceItems.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var trimmedSearch = search.Trim();
                var hasCodeFilter = int.TryParse(trimmedSearch, out var codeValue);
                var normalizedSearch = trimmedSearch.ToLower();

                query = query.Where(item =>
                    item.Value.ToLower().StartsWith(normalizedSearch) ||
                    (hasCodeFilter && item.Code == codeValue));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var skip = (page - 1) * pageSize;

            var items = await query
                .OrderBy(item => item.RowNumber)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<ReferenceItem>(items, totalCount);
        }
    }
}
