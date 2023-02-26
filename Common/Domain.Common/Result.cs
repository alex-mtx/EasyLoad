namespace Domain.Common
{
    /// <summary>
    /// Represents the result of an operation in the form of a HTTP Status Code
    /// </summary>
    public abstract record Result
    {

        /// <summary>
        /// The HTTP Status Code representing the result of the operation
        /// </summary>
        public abstract int StatusCode { get; }

        /// <summary>
        /// Represents state regarding the success of the operation
        /// </summary>
        /// <value><see cref="true"/> when sucessfull (200-299), otherwhise <see cref="false"/> </value>
        public bool Success => StatusCode > 199 && StatusCode < 299;

        /// <summary>
        /// Represents the result of an operation that does not complied with the operation's business rules
        /// </summary>
        /// <value><see cref="true"/> when there is a client error (400-499), otherwhise <see cref="false"/> </value>
        public bool HasBusinessError => StatusCode > 399 && StatusCode < 499;

        /// <summary>
        /// Represents the result of an operation that ended up in an internal error
        /// </summary>
        /// <value><see cref="true"/> when there the <see cref="StatusCode"/> is > 499, otherwhise <see cref="false"/> </value>
        public bool HasInternalError => StatusCode > 499;

        /// <summary>
        /// Creates a new instance representing a successful <see cref="OkResult"/>
        /// </summary>
        /// <returns>A <see cref="Result"/> which <see cref="Result.StatusCode"/> is 200</returns>
        public static Result CreateOk() => new OkResult();

        /// <summary>
        /// Creates a new instance representing a <see cref="NotFoundResult"/>
        /// </summary>
        /// <returns>A <see cref="Result"/> which <see cref="Result.StatusCode"/> is 404</returns>
        public static Result CreateNotFound() => new NotFoundResult();

        /// <summary>
        /// Creates a new instance representing an <see cref="InternalErrorResult"/>
        /// </summary>
        /// <returns>A <see cref="Result"/> which <see cref="Result.StatusCode"/> is 500</returns>
        public static Result CreateInternalError() => new InternalErrorResult();

        /// <summary>
        /// Creates a new instance representing an <see cref="NoContent"/>
        /// </summary>
        /// <returns>A <see cref="Result"/> which <see cref="Result.StatusCode"/> is 500</returns>
        public static Result CreateNoContent() => new NoContent();
    }
}