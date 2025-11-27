using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Codebuddy.Application.DTOs.Submissions;
using Codebuddy.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codebuddy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubmissionsController : ControllerBase
{
    private readonly ISubmissionService _submissionService;

    public SubmissionsController(ISubmissionService submissionService)
    {
        _submissionService = submissionService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<SubmissionDto>> Create([FromBody] CreateSubmissionRequest request)
    {
        try
        {
            var result = await _submissionService.CreateAsync(GetUserId(), request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<IEnumerable<SubmissionDto>>> GetMine()
    {
        var submissions = await _submissionService.GetByUserAsync(GetUserId());
        return Ok(submissions);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SubmissionDto>> GetById(Guid id)
    {
        var submission = await _submissionService.GetByIdAsync(id);
        return submission is null ? NotFound() : Ok(submission);
    }

    private Guid GetUserId()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        return Guid.TryParse(id, out var guid) ? guid : Guid.Empty;
    }
}
