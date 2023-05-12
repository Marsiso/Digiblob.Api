using Digiblob.Api.Auth.Models.Get;
using Digiblob.Api.Auth.Queries.Interfaces;

namespace Digiblob.Api.Auth.Queries;

public sealed record LoginQuery(string UserName, string Password) : IQuery<LoginResponse>;