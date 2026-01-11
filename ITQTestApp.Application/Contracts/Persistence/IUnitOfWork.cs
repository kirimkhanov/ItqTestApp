namespace ITQTestApp.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        IReferenceItemRepository ReferenceItemRepository { get; }
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
