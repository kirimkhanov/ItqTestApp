namespace ITQTestApp.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        Task ExecuteAsync(
            Func<CancellationToken, Task> action,
            CancellationToken cancellationToken);
    }
}
