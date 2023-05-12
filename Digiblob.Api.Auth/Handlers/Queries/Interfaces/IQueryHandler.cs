using Digiblob.Api.Auth.Queries.Interfaces;

namespace Digiblob.Api.Auth.Handlers.Queries.Interfaces;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}