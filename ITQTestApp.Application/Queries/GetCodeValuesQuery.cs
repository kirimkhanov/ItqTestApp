namespace ITQTestApp.Application.Queries
{
    public sealed class GetCodeValuesQuery
    {
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;
    }
}
