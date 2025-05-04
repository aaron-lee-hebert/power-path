namespace SetStats.Data.Entities;

public class UserMax
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required Guid ExerciseTypeId { get; set; }
    public required Guid TrainingCycleId { get; set; }

    public required decimal ActualOneRepMax { get; set; }
    public required decimal EstimatedOneRepMax { get; set; }
    public required decimal RoundingFactor { get; set; } = 2.5M;

    public required DateTime DateRecorded { get; set; } = DateTime.UtcNow;

    public required User User { get; set; }
    public required ExerciseType ExerciseType { get; set; }
    public required TrainingCycle TrainingCycle { get; set; }
}
