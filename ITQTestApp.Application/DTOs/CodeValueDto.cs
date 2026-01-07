namespace ITQTestApp.Application.DTOs
{
    public sealed class CodeValueDto
    {
        public int Id { get; init; }
        public int Code { get; init; }
        public string Value { get; init; } = default!;
    }
}
