using MediatR;

namespace ITQTestApp.Application.Commands
{
    public sealed class ReplaceReferenceItemsCommand(
        IReadOnlyDictionary<int, string> items) : IRequest<Unit>
    {
        public IReadOnlyDictionary<int, string> Items { get; } = items;
    }
}
