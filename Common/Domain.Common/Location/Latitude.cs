namespace Domain.Common.Location
{
    public record Latitude
    {
        public double Value { get; }

        public Latitude(double value)
        {
            if (value < -90 || value > 90) throw new ArgumentOutOfRangeException(nameof(value), $"{value} is an invalid Latitude. It should be between -90.0 and 90.0, inclusively.");
            Value = value;
        }
        public static implicit operator double(Latitude latitude) => latitude.Value;
    }

}
