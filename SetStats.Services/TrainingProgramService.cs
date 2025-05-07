using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SetStats.Core.DTOs;
using SetStats.Core.Entities;
using SetStats.Core.Interfaces.Repositories;
using SetStats.Core.Interfaces.Services;

namespace SetStats.Services;

public class TrainingProgramService(ITrainingProgramRepository repository, IHttpContextAccessor httpContextAccessor) : ITrainingProgramService
{
    private readonly ITrainingProgramRepository _repository = repository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    private Guid CurrentUserId
    {
        get
        {
            var userIdStr = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("User is not logged in.");

            return Guid.TryParse(userIdStr, out var userId)
                ? userId
                : throw new UnauthorizedAccessException("Invalid user ID.");
        }
    }

    public async Task<IEnumerable<TrainingProgramDto>> GetAllTrainingProgramsAsync()
    {
        var programs = await _repository.GetByUserIdAsync(CurrentUserId);

        return programs.Select(p => new TrainingProgramDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            CreatedAt = p.CreatedAt
        });
    }

    public async Task<TrainingProgramDto> GetTrainingProgramAsync(Guid id)
    {
        var program = await GetTrainingProgram(id);

        return new TrainingProgramDto
        {
            Id = program.Id,
            Name = program.Name,
            Description = program.Description,
            CreatedAt = program.CreatedAt
        };
    }

    public async Task AddTrainingProgramAsync(CreateTrainingProgramDto dto)
    {
        var program = new TrainingProgram
        {
            Id = Guid.NewGuid(),
            UserId = CurrentUserId,
            Name = dto.Name,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(program);
    }

    public async Task UpdateTrainingProgramAsync(Guid id, UpdateTrainingProgramDto dto)
    {
        var program = await GetTrainingProgram(id);

        program.Name = dto.Name;
        program.Description = dto.Description;

        await _repository.SaveChangesAsync();
    }

    public async Task DeleteTrainingProgramAsync(Guid id)
    {
        var program = await GetTrainingProgram(id);
        _repository.Remove(program);
        await _repository.SaveChangesAsync();
    }

    private async Task<TrainingProgram> GetTrainingProgram(Guid id)
    {
        var program = await _repository.GetByIdAsync(id);
        if (program == null || program.UserId != CurrentUserId)
        {
            throw new UnauthorizedAccessException("Access denied.");
        }
        return program;
    }
}
