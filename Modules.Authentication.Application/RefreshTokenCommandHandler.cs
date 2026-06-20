using Core.Application.Dispatcher;

namespace Modules.Authentication.Application;

public class RefreshTokenCommandHandler(IIdentityService identityService) : ICommandHandler<RefreshTokenCommand, AuthenticationResult>
{
    public Task<AuthenticationResult> HandleAsync(RefreshTokenCommand command)
    {
        return identityService.RotateTokenAsync(command.RefreshToken);
    }
}

