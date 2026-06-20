using Core.Application.Dispatcher;

namespace Modules.Authentication.Application;

public record LoginCommand(string Email, string Password) : ICommand<AuthenticationResult>;

