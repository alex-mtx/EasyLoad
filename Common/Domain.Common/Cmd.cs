using MediatR;

namespace Domain.Common
{
    /// <summary>
    /// Representes a command, which is used to change the state of the application.
    /// </summary>
    /// <remarks>The <see cref="IRequest{}"/> is a marker used by <see cref="IMediator"/>, 
    /// telling Mediator the request of a <see cref="Cmd"/> will be replied with a <see cref="Result"/>
    /// </remarks>
    public abstract record Cmd : IRequest<Result>{}
}
