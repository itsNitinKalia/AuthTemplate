using AuthTemplate.Application.Auth.Interfaces;
using AuthTemplate.Application.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthTemplate.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await authService.RegisterAsync(request, cancellationToken);
            return Ok(response);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await authService.LoginAsync(request, cancellationToken);
            return Ok(response);
        }
        catch (UnauthorizedAccessException exception)
        {
            return Unauthorized(new { message = exception.Message });
        }
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        return Ok(new
        {
            userId = User.FindFirst("sub")?.Value,
            email = User.FindFirst("email")?.Value,
            name = User.Identity?.Name,
            role = User.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value
        });
    }
}
