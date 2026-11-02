using AuthTemplate.Application.Auth.Interfaces;
using AuthTemplate.Application.Auth.Models;
using AuthTemplate.Domain.Entities;

namespace AuthTemplate.Application.Auth.Services;

public sealed class AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenProvider tokenProvider)
    : IAuthService
{
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var existingUser = await userRepository.GetByEmailAsync(email, cancellationToken);
        if (existingUser is not null)
        {
            throw new InvalidOperationException("A user with this email already exists.");
        }

        var user = new User
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = email,
            PasswordHash = passwordHasher.Hash(request.Password)
        };

        var createdUser = await userRepository.AddAsync(user, cancellationToken);
        return CreateResponse(createdUser);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var user = await userRepository.GetByEmailAsync(email, cancellationToken);
        if (user is null || !passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        return CreateResponse(user);
    }

    private AuthResponse CreateResponse(User user)
    {
        return new AuthResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role,
            Token = tokenProvider.Generate(user),
            CreatedAtUtc = user.CreatedAtUtc
        };
    }
}
