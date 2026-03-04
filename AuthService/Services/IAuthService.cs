using AuthService.DTOs;

namespace AuthService.Services;

public interface IAuthService
{
    RegisterResponse Register(RegisterRequest dto);
    AuthResponse Authenticate(LoginRequest dto);
}