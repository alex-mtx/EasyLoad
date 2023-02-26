namespace EasyLoad.Truckers.Domain.Common
{
    public record Country: Name
    {
        public Country(string name) : base(name)
        {
        }
    }
}