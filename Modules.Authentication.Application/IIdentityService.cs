namespace Modules.Authentication.Application;

public interface IIdentityService
{
    Task<AuthenticationResult> AuthenticateAsync(string email, string password);
    Task<AuthenticationResult> RotateTokenAsync(string refreshToken);
}

