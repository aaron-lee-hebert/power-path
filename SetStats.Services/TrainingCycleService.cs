using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SetStats.Core.DTOs;
using SetStats.Core.Entities;
using SetStats.Core.Interfaces.Repositories;
using SetStats.Core.Interfaces.Services;

namespace SetStats.Services;
public class TrainingCycleService(ITrainingCycleRepository repository, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) : ITrainingCycleService
{
    private async Task<Guid> GetCurrentUserIdAsync()
    {
        var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User ?? throw new UnauthorizedAccessException("User is not logged in."));
        return user?.Id ?? throw new UnauthorizedAccessException("User is not logged in.");
    }

    public async Task<IEnumerable<TrainingCycleDto>> GetAllTrainingCyclesAsync()
    {
        var currentUserId = await GetCurrentUserIdAsync();
        var cycles = await repository.GetByUserIdAsync(currentUserId);

        return cycles.Select(c => new TrainingCycleDto
        {
            Id = c.Id,
            CycleNumber = c.CycleNumber,
            StartDate = c.StartDate,
            EndDate = c.EndDate,
            RoundingFactor = c.RoundingFactor,
            Notes = c.Notes
        });
    }

    public async Task<TrainingCycleDto> GetTrainingCycleAsync(Guid id)
    {
        var cycle = await GetTrainingCycle(id);

        return new TrainingCycleDto
        {
            Id = cycle.Id,
            CycleNumber = cycle.CycleNumber,
            StartDate = cycle.StartDate,
            EndDate = cycle.EndDate,
            RoundingFactor = cycle.RoundingFactor,
            Notes = cycle.Notes
        };
    }

    public async Task<UpdateTrainingCycleDto> GetUpdateTrainingCycleAsync(Guid id)
    {
        var cycle = await GetTrainingCycle(id);

        return new UpdateTrainingCycleDto
        {
            Id = cycle.Id,
            CycleNumber = cycle.CycleNumber,
            StartDate = cycle.StartDate,
            EndDate = cycle.EndDate,
            RoundingFactor = cycle.RoundingFactor,
            Notes = cycle.Notes
        };
    }

    public async Task AddTrainingCycleAsync(CreateTrainingCycleDto dto)
    {
        var currentUserId = await GetCurrentUserIdAsync();
        var cycle = new TrainingCycle
        {
            Id = Guid.NewGuid(),
            UserId = currentUserId,
            CycleNumber = dto.CycleNumber,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            RoundingFactor = dto.RoundingFactor,
            Notes = dto.Notes
        };

        await repository.AddAsync(cycle);
        await repository.SaveChangesAsync();
    }

    public async Task UpdateTrainingCycleAsync(Guid id, UpdateTrainingCycleDto dto)
    {
        var cycle = await GetTrainingCycle(id);

        cycle.CycleNumber = dto.CycleNumber;
        cycle.StartDate = dto.StartDate;
        cycle.EndDate = dto.EndDate;
        cycle.RoundingFactor = dto.RoundingFactor;
        cycle.Notes = dto.Notes;

        await repository.SaveChangesAsync();
    }

    public async Task DeleteTrainingCycleAsync(Guid id)
    {
        var cycle = await GetTrainingCycle(id);
        repository.Remove(cycle);
        await repository.SaveChangesAsync();
    }

    private async Task<TrainingCycle> GetTrainingCycle(Guid id)
    {
        var currentUserId = await GetCurrentUserIdAsync();
        var cycle = await repository.GetByIdAsync(id);

        if (cycle == null || cycle.UserId != currentUserId)
        {
            throw new UnauthorizedAccessException("Access denied.");
        }
        return cycle;
    }
}
