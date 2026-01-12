using ITQTestApp.Application.Common.Pagination;
using ITQTestApp.Application.Contracts.Persistence;
using ITQTestApp.Application.DTOs;
using ITQTestApp.Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ITQTestApp.Application.Handlers
{
    public sealed class GetReferenceItemsHandler
        : IRequestHandler<GetReferenceItemsQuery, PagedResult<ReferenceItemDto>>
    {
        private readonly IReferenceItemRepository _repository;
        private readonly ILogger<GetReferenceItemsHandler> _logger;

        public GetReferenceItemsHandler(
            IReferenceItemRepository repository,
            ILogger<GetReferenceItemsHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<PagedResult<ReferenceItemDto>> Handle(
            GetReferenceItemsQuery query,
            CancellationToken cancellationToken)
        {
            if (query.Page <= 0)
            {
                _logger.LogWarning("Invalid page requested: {Page}", query.Page);
                throw new ArgumentException("Номер страницы должен быть больше нуля");
            }

            if (query.PageSize <= 0)
            {
                _logger.LogWarning("Invalid page size requested: {PageSize}", query.PageSize);
                throw new ArgumentException("Размер страницы должен быть больше нуля");
            }

            _logger.LogInformation(
                "Loading reference items. Page: {Page}, PageSize: {PageSize}, Search: {Search}",
                query.Page,
                query.PageSize,
                query.Search);

            var result = await _repository.GetAsync(
                query.Page,
                query.PageSize,
                query.Search,
                cancellationToken);

            var dtoItems = result.Items
                .Select(item => new ReferenceItemDto
                {
                    RowNumber = item.RowNumber,
                    Code = item.Code.Value,
                    Value = item.Value
                })
                .ToList();

            _logger.LogInformation(
                "Reference items loaded. Returned: {ReturnedCount}, Total: {TotalCount}",
                dtoItems.Count,
                result.TotalCount);

            return new PagedResult<ReferenceItemDto>(
                dtoItems,
                result.TotalCount);
        }
    }
}

