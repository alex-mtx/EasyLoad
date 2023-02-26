namespace Domain.Common
{
    public record Id
    {
        public string Value { get; }

        public Id(string value)
        {
            Validate(value);
            Value = value;
        }

        protected virtual void Validate(string value)
        {
            if (value is null || value.Length == 1 || value.Length > 50) throw new ArgumentException($"'{value}' is invalid. It must be between 1 and 50 characters long.", nameof(value));

        }

        public static implicit operator string(Id value) => value.Value;
        public static implicit operator Id(string value) => new(value);
    }
}