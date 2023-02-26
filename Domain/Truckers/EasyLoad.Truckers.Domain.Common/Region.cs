namespace EasyLoad.Truckers.Domain.Common
{
    public record Region : Name
    {
        public Region(string name) : base(name)
        {
        }
    }
}