using ITQTestApp.Domain.ValueObjects;

namespace ITQTestApp.Domain.Entities
{
    public class ReferenceItem
    {
        public int Id { get; private set; }

        public Code Code { get; private set; }

        public string Value { get; private set; }

        private ReferenceItem() { }

        public ReferenceItem(Code code, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Значение не может быть пустым", nameof(value));

            Code = code;
            Value = value;
        }
    }
}
