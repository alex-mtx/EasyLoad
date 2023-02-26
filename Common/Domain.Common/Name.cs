namespace EasyLoad.Truckers.Domain.Common
{
    public record Name
    {
        public string Value { get; }

        public Name(string value)
        {
            Validate(value);
            Value = value;
        }

        protected virtual void Validate(string value)
        {
            if (value is null || value.Length == 1 || value.Length > 50) throw new ArgumentException($"'{value}' is invalid. It must be between 1 and 50 characters long.", nameof(value));

        }

        public static implicit operator string(Name value) => value.Value;
    }
}