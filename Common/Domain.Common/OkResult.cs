namespace Domain.Common
{
    /// <summary>
    /// An <see cref="OkResult"/> represents a <see cref="StatusCodes.Status200OK"/>
    /// </summary>
    public record OkResult : Result
    {
        public override int StatusCode => StatusCodes.Status200OK;
    }
}