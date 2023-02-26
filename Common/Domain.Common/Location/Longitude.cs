namespace Domain.Common.Location
{
    public record Longitude
    {
        public double Value { get; }

        public Longitude(double value)
        {
            if (value < -180 || value > 180) throw new ArgumentOutOfRangeException(nameof(value), $"{value} is an invalid Longitude. It should be between -180.0 and 180.0, inclusively.");
            Value = value;
        }
        public static implicit operator double(Longitude longitude) => longitude.Value;

    }
}
