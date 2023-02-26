using Domain.Common;

namespace Common.Queries
{
    public abstract record QueryResponse<T>
    {
        public abstract T? Value { get; }

        public QueryResponse(Result state)
        {
            State = state;
        }

        public Result State { get; } = Result.CreateOk();
        public bool Success => State.Success;
        public bool HasBusinessError => State.HasBusinessError;
        public bool HasInternalError => State.HasInternalError;
        public int StatusCode => State.StatusCode;
    }
}