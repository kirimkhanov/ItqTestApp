namespace ITQTestApp.Domain.ValueObjects
{
    public sealed class Code : IEquatable<Code>
    {
        public int Value { get; }

        public Code(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Код должен быть положительным числом", nameof(value));

            Value = value;
        }

        public bool Equals(Code? other)
            => other is not null && Value == other.Value;

        public override bool Equals(object? obj)
            => obj is Code other && Equals(other);

        public override int GetHashCode()
            => Value.GetHashCode();

        public static implicit operator int(Code code) => code.Value;
    }
}
