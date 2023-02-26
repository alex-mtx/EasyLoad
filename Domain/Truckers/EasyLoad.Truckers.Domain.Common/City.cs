namespace EasyLoad.Truckers.Domain.Common
{
    public record City : Name
    {
        public City(string name) : base(name)
        {
        }
    }
}