using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Common.Handlers
{
    public abstract class Handler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger _logger;

        protected Handler(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await HandleInternally(request, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, null);
                return await DefaultErrorResponse();
            }
        }

        protected abstract Task<TResponse> HandleInternally(TRequest request, CancellationToken cancellationToken);
        protected abstract Task<TResponse> DefaultErrorResponse();
    }
}
