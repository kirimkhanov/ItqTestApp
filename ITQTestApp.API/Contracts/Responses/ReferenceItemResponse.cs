namespace ITQTestApp.API.Contracts.Responses
{
    public class ReferenceItemResponse
    {
        public int RowNumber { get; init; }
        public int Code { get; init; }
        public string Value { get; init; } = string.Empty;
    }
}
