namespace TruckersApi.Queries
{
    public record Location
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}