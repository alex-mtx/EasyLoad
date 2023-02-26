namespace EasyLoad.Truckers.Domain.Common
{
    public record LastName : Name
    {
        public LastName(string original) : base(original)
        {
        }
    }
}