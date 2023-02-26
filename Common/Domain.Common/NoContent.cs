namespace Domain.Common
{
    /// <summary>
    /// A <see cref="NoContent"/> represents a <see cref="StatusCodes.Status204NoContent"/>
    /// </summary>
    public record NoContent : Result
    {
        public override int StatusCode => StatusCodes.Status204NoContent;
    }
}