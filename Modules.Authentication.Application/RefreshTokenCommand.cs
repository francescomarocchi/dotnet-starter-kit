using Core.Application.Dispatcher;

namespace Modules.Authentication.Application;

public record RefreshTokenCommand(string RefreshToken) : ICommand<AuthenticationResult>;

