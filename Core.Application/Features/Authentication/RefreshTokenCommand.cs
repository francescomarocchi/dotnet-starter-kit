using Core.Application.Dispatcher;

namespace Core.Application.Features.Authentication;

public record RefreshTokenCommand(string RefreshToken) : ICommand<AuthenticationResult>;