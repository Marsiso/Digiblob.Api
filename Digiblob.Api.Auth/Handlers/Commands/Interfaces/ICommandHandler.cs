using Digiblob.Api.Auth.Commands.Interfaces;

namespace Digiblob.Api.Auth.Handlers.Commands.Interfaces;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}