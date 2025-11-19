using Dtos;
using Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<bool> RegisterAsync(RegisterDto dto)
    {
        var existing = await _userRepository.GetByEmailAsync(dto.Email);
        if (existing is not null)
            throw new InvalidOperationException("User with this email already exists");

        var user = new User
        {
            Email = dto.Email,
            Password = dto.Password,  
            Role = dto.Role
        };

        await _userRepository.CreateAsync(user);
        return true;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user is null) return null;

        if (user.Password != dto.Password) return null;

        var access = _jwtService.GenerateAccessToken(user);

        return new AuthResponseDto
        {
            AccessToken = access,
            RefreshToken = ""
        };
    }
}
