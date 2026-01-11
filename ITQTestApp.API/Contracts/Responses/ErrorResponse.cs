namespace ITQTestApp.API.Contracts.Responses
{
    public sealed class ErrorResponse
    {
        public string Code { get; init; } = null!;
        public string Message { get; init; } = null!;
    }
}
