using Core.Application.Dispatcher;

namespace Modules.Authentication.Application;

public class LoginCommandHandler(IIdentityService identityService) : ICommandHandler<LoginCommand, AuthenticationResult>
{
    public Task<AuthenticationResult> HandleAsync(LoginCommand command)
    {
        return identityService.AuthenticateAsync(command.Email, command.Password);
    }
}

