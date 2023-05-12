namespace Digiblob.Api.Auth.Commands.Interfaces;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}