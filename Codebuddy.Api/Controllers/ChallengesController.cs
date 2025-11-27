using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Codebuddy.Application.DTOs.Challenges;
using Codebuddy.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codebuddy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChallengesController : ControllerBase
{
    private readonly IChallengeService _challengeService;

    public ChallengesController(IChallengeService challengeService)
    {
        _challengeService = challengeService;
    }

    [HttpGet]
    public async Task<ActionResult> GetChallenges([FromQuery] ChallengeFilterRequest filter)
    {
        var result = await _challengeService.GetChallengesAsync(filter);
        return Ok(result);
    }

    [HttpGet("{slug}")]
    public async Task<ActionResult<ChallengeDto>> GetBySlug(string slug)
    {
        var challenge = await _challengeService.GetBySlugAsync(slug);
        return challenge is null ? NotFound() : Ok(challenge);
    }

    [HttpGet("{id:guid}/tests")]
    public async Task<ActionResult<IEnumerable<ChallengeTestDto>>> GetTests(Guid id)
    {
        var tests = await _challengeService.GetTestsAsync(id);
        return Ok(tests);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ChallengeDto>> Create([FromBody] CreateChallengeRequest request)
    {
        try
        {
            var challenge = await _challengeService.CreateAsync(request, GetUserId());
            return CreatedAtAction(nameof(GetBySlug), new { slug = challenge.Slug }, challenge);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private Guid GetUserId()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        return Guid.TryParse(id, out var guid) ? guid : Guid.Empty;
    }
}
