using Codebuddy.Application.DTOs.Discussions;

namespace Codebuddy.Application.Interfaces;

public interface IDiscussionService
{
    Task<IEnumerable<DiscussionThreadDto>> GetThreadsAsync(Guid challengeId);
    Task<DiscussionThreadDto> CreateThreadAsync(Guid challengeId, Guid userId, CreateThreadRequest request);
    Task<IEnumerable<DiscussionMessageDto>> GetMessagesAsync(Guid threadId);
    Task<DiscussionMessageDto> CreateMessageAsync(Guid threadId, Guid userId, CreateMessageRequest request);
}
