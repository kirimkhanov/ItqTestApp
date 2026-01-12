using ITQTestApp.Application.Commands;
using ITQTestApp.Application.Contracts.Persistence;
using ITQTestApp.Domain.Entities;
using ITQTestApp.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ITQTestApp.Application.Handlers
{
    public sealed class ReplaceReferenceItemsHandler
        : IRequestHandler<ReplaceReferenceItemsCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReferenceItemRepository _referenceItemRepository;
        private readonly ILogger<ReplaceReferenceItemsHandler> _logger;

        public ReplaceReferenceItemsHandler(
            IUnitOfWork unitOfWork,
            IReferenceItemRepository referenceItemRepository,
            ILogger<ReplaceReferenceItemsHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _referenceItemRepository = referenceItemRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            ReplaceReferenceItemsCommand command,
            CancellationToken cancellationToken)
        {
            ValidateItems(command.Items);

            _logger.LogInformation("Replacing reference items. Count: {ItemCount}", command.Items.Count);

            var entities = command.Items
                .OrderBy(i => i.Key)
                .Select((item, index) => new ReferenceItem(
                    code: new Code(item.Key),
                    value: item.Value,
                    rowNumber: index + 1))
                .ToList();

            await _unitOfWork.ExecuteAsync(async ct =>
            {
                await _referenceItemRepository.ClearAsync(ct);
                await _referenceItemRepository.AddRangeAsync(entities, ct);
            }, cancellationToken);

            _logger.LogInformation("Reference items replaced. Saved: {SavedCount}", entities.Count);

            return Unit.Value;
        }

        private static void ValidateItems(IReadOnlyDictionary<int, string> items)
        {
            if (items is null || items.Count == 0)
                throw new ArgumentException("Список элементов для загрузки пуст.");

            foreach (var (code, value) in items)
            {
                if (code <= 0)
                    throw new ArgumentException($"Код {code} должен быть положительным.");

                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException($"Значение не должно быть пустым.");
            }
        }
    }
}

