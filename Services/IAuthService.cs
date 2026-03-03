using RESTApi.DTOs;

namespace RESTApi.Services;

public interface IAuthService
{
    Task<AuthResponseDto?> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto?> LoginAsync(LoginDto dto);
}
