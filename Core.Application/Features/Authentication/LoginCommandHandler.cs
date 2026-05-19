using Core.Application.Dispatcher;

namespace Core.Application.Features.Authentication;

public class LoginCommandHandler(IIdentityService identityService) : ICommandHandler<LoginCommand, AuthenticationResult>
{
    public Task<AuthenticationResult> HandleAsync(LoginCommand command)
    {
        return identityService.AuthenticateAsync(command.Email, command.Password);
    }
}