using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Codebuddy.Application.DTOs.Buddies;
using Codebuddy.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codebuddy.Api.Controllers;

[ApiController]
[Route("api/buddies/sessions")]
public class BuddySessionsController : ControllerBase
{
    private readonly IBuddyService _buddyService;

    public BuddySessionsController(IBuddyService buddyService)
    {
        _buddyService = buddyService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<BuddySessionDto>> Create([FromBody] CreateBuddySessionRequest request)
    {
        try
        {
            var session = await _buddyService.CreateSessionAsync(GetUserId(), request);
            return Ok(session);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<IEnumerable<BuddySessionDto>>> GetMine()
    {
        var sessions = await _buddyService.GetUserSessionsAsync(GetUserId());
        return Ok(sessions);
    }

    private Guid GetUserId()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        return Guid.TryParse(id, out var guid) ? guid : Guid.Empty;
    }
}
