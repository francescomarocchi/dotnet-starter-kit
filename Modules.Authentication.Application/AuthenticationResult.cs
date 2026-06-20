namespace Modules.Authentication.Application;

public record AuthenticationResult(
    string? AccessToken,
    string? TokenType,
    int? ExpiresIn,
    string? RefreshToken)
{
    public static AuthenticationResult Empty => new(null, null, null, null);
}

