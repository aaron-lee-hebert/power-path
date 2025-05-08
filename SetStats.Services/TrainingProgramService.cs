using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SetStats.Core.DTOs;
using SetStats.Core.Entities;
using SetStats.Core.Interfaces.Repositories;
using SetStats.Core.Interfaces.Services;

namespace SetStats.Services;

public class TrainingProgramService(ITrainingProgramRepository repository, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) : ITrainingProgramService
{
    private async Task<Guid> GetCurrentUserIdAsync()
    {
        var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User ?? throw new UnauthorizedAccessException("User is not logged in."));
        return user?.Id ?? throw new UnauthorizedAccessException("User is not logged in.");
    }

    public async Task<IEnumerable<TrainingProgramDto>> GetAllTrainingProgramsAsync()
    {
        var currentUserId = await GetCurrentUserIdAsync();
        var programs = await repository.GetByUserIdAsync(currentUserId);

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

    public async Task<UpdateTrainingProgramDto> GetUpdateTrainingProgramAsync(Guid id)
    {
        var program = await GetTrainingProgram(id);

        return new UpdateTrainingProgramDto
        {
            Id = program.Id,
            Name = program.Name,
            Description = program.Description,
            IsActive = program.IsActive
        };
    }

    public async Task AddTrainingProgramAsync(CreateTrainingProgramDto dto)
    {
        var currentUserId = await GetCurrentUserIdAsync();
        var program = new TrainingProgram
        {
            Id = Guid.NewGuid(),
            UserId = currentUserId,
            Name = dto.Name,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(program);
        await repository.SaveChangesAsync();
    }

    public async Task UpdateTrainingProgramAsync(Guid id, UpdateTrainingProgramDto dto)
    {
        var program = await GetTrainingProgram(id);

        program.Name = dto.Name;
        program.Description = dto.Description;

        await repository.SaveChangesAsync();
    }

    public async Task DeleteTrainingProgramAsync(Guid id)
    {
        var program = await GetTrainingProgram(id);
        repository.Remove(program);
        await repository.SaveChangesAsync();
    }

    private async Task<TrainingProgram> GetTrainingProgram(Guid id)
    {
        var currentUserId = await GetCurrentUserIdAsync();
        var program = await repository.GetByIdAsync(id);

        if (program == null || program.UserId != currentUserId)
        {
            throw new UnauthorizedAccessException("Access denied.");
        }
        return program;
    }
}
