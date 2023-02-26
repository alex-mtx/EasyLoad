namespace Domain.Common.Location
{
    public record DistanceKm
    {
        public DistanceKm(int value)
        {

            if (value < 0 || value > Max) throw new ArgumentOutOfRangeException(nameof(value), $"{value} is an invalid Distance. It should be between 0 and {Max} Kilometers, inclusively.");
            Value = value;
        }
        public const int Max = 1000;
        public int Value { get; }
        public int GetMeters => Value * 1000;
    }
}
