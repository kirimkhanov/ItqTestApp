using ITQTestApp.Application.Common.Pagination;
using ITQTestApp.Application.DTOs;
using MediatR;

namespace ITQTestApp.Application.Queries
{
    public sealed class GetReferenceItemsQuery(int page, int pageSize, string? search)
        : IRequest<PagedResult<ReferenceItemDto>>
    {
        public int Page { get; init; } = page;
        public int PageSize { get; init; } = pageSize;
        public string? Search { get; init; } = search;
    }
}
