using RESTApi.DTOs;

namespace RESTApi.Services;

/// <summary>
/// Service interface for user authentication.
/// </summary>
public interface IAuthService
{
    Task<AuthResponseDto?> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto?> LoginAsync(LoginDto dto);
}
