using ITQTestApp.Application.Commands;
using ITQTestApp.Application.Contracts.Persistence;
using ITQTestApp.Domain.Entities;
using ITQTestApp.Domain.ValueObjects;

namespace ITQTestApp.Application.Handlers
{
    public sealed class UploadCodeValuesHandler
    {
        private readonly ICodeValueRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UploadCodeValuesHandler(ICodeValueRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(
            UploadCodeValuesCommand command,
            CancellationToken cancellationToken)
        {
            if (command.Items is null || command.Items.Count == 0)
                throw new ArgumentException("Список элементов для загрузки пуст");

            var entities = command.Items
                .OrderBy(i => i.Key)
                .Select(i => new CodeValueItem(
                    code: new Code(i.Key),
                    value: i.Value))
                .ToList();

            await _unitOfWork.ExecuteAsync(async ct =>
            {
                await _repository.ClearAsync(ct);
                await _repository.AddRangeAsync(entities, ct);
            }, cancellationToken);
        }
    }
}
