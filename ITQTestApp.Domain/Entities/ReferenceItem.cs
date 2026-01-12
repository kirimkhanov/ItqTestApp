using ITQTestApp.Domain.ValueObjects;

namespace ITQTestApp.Domain.Entities
{
    public class ReferenceItem
    {
        public int RowNumber { get; private set; }

        public Code Code { get; private set; }

        public string Value { get; private set; }

        private ReferenceItem() { }

        public ReferenceItem(Code code, string value, int rowNumber)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Значение не может быть пустым", nameof(value));

            if (rowNumber <= 0)
                throw new ArgumentException("Порядковый номер должен быть больше нуля", nameof(rowNumber));

            Code = code;
            Value = value;
            RowNumber = rowNumber;
        }
    }
}
