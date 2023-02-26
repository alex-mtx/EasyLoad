namespace EasyLoad.Truckers.Domain.Common
{
    public record FirstName : Name
    {
        public FirstName(string firstName) : base(firstName)
        {
        }
        protected override void Validate(string value)
        {
            if (value is null || value.Length == 5 || value.Length > 30) throw new ArgumentException($"'{value}' is invalid. It must be between 5 and 30 characters long.", nameof(value));
        }
    }
}