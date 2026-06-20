using Modules.Authentication.Application;

namespace Modules.Authentication.Infrastructure;

public class FakeIdentityService : IIdentityService
{
    private static string _currentValidRefreshToken = "valid_refresh_token_xyz";

    public Task<AuthenticationResult> AuthenticateAsync(string email, string password)
    {
        if (email != "admin@test.com" || password != "password123")
            return Task.FromResult<AuthenticationResult>(AuthenticationResult.Empty);
        var result = new AuthenticationResult(
            AccessToken: "fake_jwt_access_token_for_admin",
            TokenType: "Bearer",
            ExpiresIn: 900,
            RefreshToken: _currentValidRefreshToken
        );
        return Task.FromResult(result);
    }

    public Task<AuthenticationResult> RotateTokenAsync(string refreshToken)
    {
        if (refreshToken != _currentValidRefreshToken)
            return Task.FromResult<AuthenticationResult>(AuthenticationResult.Empty);
        _currentValidRefreshToken = "new_refresh_token_abc_" + Guid.NewGuid().ToString()[..4];

        var result = new AuthenticationResult(
            AccessToken: "fake_refreshed_jwt_access_token",
            TokenType: "Bearer",
            ExpiresIn: 900,
            RefreshToken: _currentValidRefreshToken
        );
        return Task.FromResult(result);
    }
}

