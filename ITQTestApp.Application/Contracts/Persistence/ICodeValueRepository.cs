using ITQTestApp.Application.Common.Pagination;
using ITQTestApp.Application.Queries;
using ITQTestApp.Domain.Entities;

namespace ITQTestApp.Application.Contracts.Persistence
{
    public interface ICodeValueRepository
    {
        Task ClearAsync(CancellationToken cancellationToken);

        Task AddRangeAsync(
            IReadOnlyCollection<CodeValueItem> items,
            CancellationToken cancellationToken);

        Task<PagedResult<CodeValueItem>> GetAsync(
            GetCodeValuesQuery query,
            CancellationToken cancellationToken);
    }
}
