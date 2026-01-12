using ITQTestApp.Application.Common.Pagination;
using ITQTestApp.Application.Contracts.Persistence;
using ITQTestApp.Application.DTOs;
using ITQTestApp.Application.Queries;
using MediatR;

namespace ITQTestApp.Application.Handlers
{
    public sealed class GetReferenceItemsHandler
        : IRequestHandler<GetReferenceItemsQuery, PagedResult<ReferenceItemDto>>
    {
        private readonly IReferenceItemRepository _repository;

        public GetReferenceItemsHandler(IReferenceItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<ReferenceItemDto>> Handle(
            GetReferenceItemsQuery query,
            CancellationToken cancellationToken)
        {
            if (query.Page <= 0)
                throw new ArgumentException("Номер страницы должен быть больше нуля");

            if (query.PageSize <= 0)
                throw new ArgumentException("Размер страницы должен быть больше нуля");

            var result = await _repository.GetAsync(
                query.Page,
                query.PageSize,
                cancellationToken);

            var dtoItems = result.Items
                .Select(item => new ReferenceItemDto
                {
                    RowNumber = item.RowNumber,
                    Code = item.Code.Value,
                    Value = item.Value
                })
                .ToList();

            return new PagedResult<ReferenceItemDto>(
                dtoItems,
                result.TotalCount);
        }
    }
}

