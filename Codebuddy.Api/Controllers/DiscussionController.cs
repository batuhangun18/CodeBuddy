using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Codebuddy.Application.DTOs.Discussions;
using Codebuddy.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codebuddy.Api.Controllers;

[ApiController]
[Route("api")]
public class DiscussionController : ControllerBase
{
    private readonly IDiscussionService _discussionService;

    public DiscussionController(IDiscussionService discussionService)
    {
        _discussionService = discussionService;
    }

    [HttpGet("challenges/{challengeId:guid}/threads")]
    public async Task<ActionResult<IEnumerable<DiscussionThreadDto>>> GetThreads(Guid challengeId)
    {
        var threads = await _discussionService.GetThreadsAsync(challengeId);
        return Ok(threads);
    }

    [Authorize]
    [HttpPost("challenges/{challengeId:guid}/threads")]
    public async Task<ActionResult<DiscussionThreadDto>> CreateThread(Guid challengeId, [FromBody] CreateThreadRequest request)
    {
        try
        {
            var thread = await _discussionService.CreateThreadAsync(challengeId, GetUserId(), request);
            return Ok(thread);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("threads/{threadId:guid}/messages")]
    public async Task<ActionResult<IEnumerable<DiscussionMessageDto>>> GetMessages(Guid threadId)
    {
        var messages = await _discussionService.GetMessagesAsync(threadId);
        return Ok(messages);
    }

    [Authorize]
    [HttpPost("threads/{threadId:guid}/messages")]
    public async Task<ActionResult<DiscussionMessageDto>> CreateMessage(Guid threadId, [FromBody] CreateMessageRequest request)
    {
        try
        {
            var message = await _discussionService.CreateMessageAsync(threadId, GetUserId(), request);
            return Ok(message);
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
