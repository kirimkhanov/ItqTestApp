namespace ITQTestApp.API.Contracts.Requests
{
    public class ReplaceReferenceItemsRequest
    {
        public IReadOnlyDictionary<int, string> Items { get; init; }
    }
}
