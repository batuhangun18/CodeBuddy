using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Codebuddy.Application.DTOs.Users;
using Codebuddy.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codebuddy.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserProfileDto>> GetMe()
    {
        var userId = GetUserId();
        var user = await _userService.GetCurrentUserAsync(userId);
        return user is null ? NotFound() : Ok(user);
    }

    [Authorize]
    [HttpPut("me")]
    public async Task<ActionResult<UserProfileDto>> UpdateMe([FromBody] UpdateProfileRequest request)
    {
        var userId = GetUserId();
        var updated = await _userService.UpdateProfileAsync(userId, request);
        return updated is null ? NotFound() : Ok(updated);
    }

    private Guid GetUserId()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        return Guid.TryParse(id, out var guid) ? guid : Guid.Empty;
    }
}
