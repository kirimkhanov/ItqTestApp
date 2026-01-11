using ITQTestApp.Application.Common.Pagination;
using ITQTestApp.Domain.Entities;

namespace ITQTestApp.Application.Contracts.Persistence
{
    public interface IReferenceItemRepository
    {
        Task ClearAsync(CancellationToken cancellationToken);

        Task AddRangeAsync(
            IReadOnlyCollection<ReferenceItem> items,
            CancellationToken cancellationToken);

        Task<PagedResult<ReferenceItem>> GetAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken);
    }
}
