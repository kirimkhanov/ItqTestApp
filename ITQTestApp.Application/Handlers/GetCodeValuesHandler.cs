using ITQTestApp.Application.Common.Pagination;
using ITQTestApp.Application.Contracts.Persistence;
using ITQTestApp.Application.DTOs;
using ITQTestApp.Application.Queries;

namespace ITQTestApp.Application.Handlers
{
    public sealed class GetCodeValuesHandler
    {
        private readonly ICodeValueRepository _repository;

        public GetCodeValuesHandler(ICodeValueRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<CodeValueDto>> HandleAsync(
            GetCodeValuesQuery query,
            CancellationToken cancellationToken)
        {
            if (query.Page <= 0)
                throw new ArgumentException("Номер страницы должен быть больше нуля");

            if (query.PageSize <= 0)
                throw new ArgumentException("Размер страницы должен быть больше нуля");

            var result = await _repository.GetAsync(query, cancellationToken);

            var dtoItems = result.Items
                .Select(x => new CodeValueDto
                {
                    Id = x.Id,
                    Code = x.Code.Value,
                    Value = x.Value
                })
                .ToList();

            return new PagedResult<CodeValueDto>(
                dtoItems,
                result.TotalCount);
        }
    }
}
