using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TestController : ControllerBase
{
    [HttpGet("me")]
    [Authorize]
    public ActionResult<object> GetMe()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email);
        var role = User.FindFirstValue(ClaimTypes.Role);

        return Ok(new
        {
            Id = userId,
            Email = email,
            Role = role
        });
    }

    [HttpGet("admin-only")]
    [Authorize(Roles = "Admin")]
    public ActionResult<string> AdminOnly()
    {
        return Ok("Привіт, адмін 👑");
    }
}
