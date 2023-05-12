using Digiblob.Api.Auth.Commands.Interfaces;

namespace Digiblob.Api.Auth.Commands;

public sealed record CreateUserCommand(string GivenName, string FamilyName, string Email, string Password, string UserName) : ICommand<Unit>;