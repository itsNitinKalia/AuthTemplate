namespace AuthTemplate.Application.Auth.Models;

public sealed class AuthResponse
{
    public int Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
    public DateTime CreatedAtUtc { get; init; }
}
