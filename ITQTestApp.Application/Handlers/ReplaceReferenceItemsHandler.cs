using ITQTestApp.Application.Commands;
using ITQTestApp.Application.Contracts.Persistence;
using ITQTestApp.Application.Exceptions;
using ITQTestApp.Domain.Entities;
using ITQTestApp.Domain.ValueObjects;
using MediatR;

namespace ITQTestApp.Application.Handlers
{
    public sealed class ReplaceReferenceItemsHandler
        : IRequestHandler<ReplaceReferenceItemsCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReplaceReferenceItemsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(
            ReplaceReferenceItemsCommand command,
            CancellationToken cancellationToken)
        {
            if (command.Items is null || command.Items.Count == 0)
                throw new ValidationException("Список элементов для загрузки пуст");

            var entities = command.Items
                .OrderBy(i => i.Key)
                .Select(i => new ReferenceItem(
                    code: new Code(i.Key),
                    value: i.Value))
                .ToList();

            await _unitOfWork.ReferenceItemRepository.ClearAsync(cancellationToken);
            await _unitOfWork.ReferenceItemRepository.AddRangeAsync(entities, cancellationToken);

            await _unitOfWork.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
