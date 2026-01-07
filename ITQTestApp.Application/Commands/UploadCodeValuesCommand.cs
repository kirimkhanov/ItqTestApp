namespace ITQTestApp.Application.Commands
{
    public sealed class UploadCodeValuesCommand(
        IReadOnlyDictionary<int, string> items)
    {
        public IReadOnlyDictionary<int, string> Items { get; } = items;
    }
}
