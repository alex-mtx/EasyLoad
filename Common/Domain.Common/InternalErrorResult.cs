namespace Domain.Common
{
    /// <summary>
    /// A <see cref="InternalErrorResult"/> represents a <see cref="StatusCodes.Status500InternalServerError"/>
    /// </summary>
    public record InternalErrorResult : Result
    {
        public override int StatusCode => StatusCodes.Status500InternalServerError;
    }
}