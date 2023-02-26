namespace Domain.Common
{
    /// <summary>
    /// A <see cref="NotFoundResult"/> represents a <see cref="StatusCodes.Status404NotFound"/>
    /// </summary>
    public record NotFoundResult : Result
    {
        public override int StatusCode => StatusCodes.Status404NotFound;
    }
}