namespace ITQTestApp.API.Contracts.Requests
{
    public class GetReferenceItemsRequest
    {
        public int Page { get; init; }
        public int PageSize { get; init; }
        public string? Search { get; init; }
    }
}
