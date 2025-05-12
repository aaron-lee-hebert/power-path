using SetStats.Core.DTOs;

namespace SetStats.Core.Interfaces.Services;
public interface ITrainingCycleService
{
    Task<TrainingCycleDto> GetTrainingCycleAsync(Guid id);
    Task<UpdateTrainingCycleDto> GetUpdateTrainingCycleAsync(Guid id);
    Task<IEnumerable<TrainingCycleDto>> GetAllTrainingCyclesAsync();
    Task AddTrainingCycleAsync(CreateTrainingCycleDto dto);
    Task UpdateTrainingCycleAsync(Guid id, UpdateTrainingCycleDto dto);
    Task DeleteTrainingCycleAsync(Guid id);
}
