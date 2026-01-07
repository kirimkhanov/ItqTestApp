namespace ITQTestApp.Application.Common.Pagination
{
    public sealed class PagedResult<T>(
        IReadOnlyCollection<T> items,
        int totalCount)
    {
        public IReadOnlyCollection<T> Items { get; } = items;
        public int TotalCount { get; } = totalCount;
    }
}
