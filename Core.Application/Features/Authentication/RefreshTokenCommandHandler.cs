using Core.Application.Dispatcher;

namespace Core.Application.Features.Authentication;

public class RefreshTokenCommandHandler(IIdentityService identityService): ICommandHandler<RefreshTokenCommand, AuthenticationResult>
{
    public Task<AuthenticationResult> HandleAsync(RefreshTokenCommand command)
    {
        return identityService.RotateTokenAsync(command.RefreshToken);
    }
}