using Core.Application.Dispatcher;

namespace Core.Application.Features.Authentication;

public record LoginCommand(string Email, string Password) : ICommand<AuthenticationResult>;