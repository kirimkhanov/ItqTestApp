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
        private readonly ILogger<ReplaceReferenceItemsHandler> _logger;

        public ReplaceReferenceItemsHandler(
            IUnitOfWork unitOfWork,
            ILogger<ReplaceReferenceItemsHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            ReplaceReferenceItemsCommand command,
            CancellationToken cancellationToken)
        {
            if (command.Items is null || command.Items.Count == 0)
                throw new ArgumentException("Список элементов для загрузки пуст");

            _logger.LogInformation("Replacing reference items. Count: {ItemCount}", command.Items.Count);

            var entities = command.Items
                .OrderBy(i => i.Key)
                .Select((item, index) => new ReferenceItem(
                    code: new Code(item.Key),
                    value: item.Value,
                    rowNumber: index + 1))
                .ToList();

            await _unitOfWork.ReferenceItemRepository.ClearAsync(cancellationToken);
            await _unitOfWork.ReferenceItemRepository.AddRangeAsync(entities, cancellationToken);

            var savedCount = await _unitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("Reference items replaced. Saved: {SavedCount}", savedCount);

            return Unit.Value;
        }
    }
}
