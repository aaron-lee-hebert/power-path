using SetStats.Core.DTOs;

namespace SetStats.Core.Interfaces.Services;
public interface ITrainingProgramService
{
    Task<TrainingProgramDto> GetTrainingProgramAsync(Guid id);
    Task<IEnumerable<TrainingProgramDto>> GetAllTrainingProgramsAsync();
    Task AddTrainingProgramAsync(CreateTrainingProgramDto dto);
    Task UpdateTrainingProgramAsync(Guid id, UpdateTrainingProgramDto dto);
    Task DeleteTrainingProgramAsync(Guid id);
}
