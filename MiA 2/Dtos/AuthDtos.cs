using Models;

namespace Dtos;

public class RegisterDto
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public UserRoles Role { get; set; } = UserRoles.User;
}

public class LoginDto
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}

public class AuthResponseDto
{
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";
}

public class RefreshRequestDto
{
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";
}
