using ITQTestApp.Domain.ValueObjects;

namespace ITQTestApp.Domain.Entities
{
    public class CodeValueItem
    {
        public int Id { get; private set; }

        public Code Code { get; private set; }

        public string Value { get; private set; }

        private CodeValueItem() { }

        public CodeValueItem(Code code, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Значение не может быть пустым", nameof(value));

            Code = code;
            Value = value;
        }
    }
}
