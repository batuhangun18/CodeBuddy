using Codebuddy.Application.DTOs.Submissions;

namespace Codebuddy.Application.Interfaces;

public interface ISubmissionService
{
    Task<SubmissionDto> CreateAsync(Guid userId, CreateSubmissionRequest request);
    Task<IEnumerable<SubmissionDto>> GetByUserAsync(Guid userId);
    Task<SubmissionDto?> GetByIdAsync(Guid id);
}
