namespace SetStats.Core.Entities;

public class ProgressRecord
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required Guid ExerciseTypeId { get; set; }

    public required DateTime RecordDate { get; set; } = DateTime.UtcNow;
    public required decimal Weight { get; set; }
    public required int Reps { get; set; }
    public required bool IsPersonalRecord { get; set; }

    public required User User { get; set; }
    public required ExerciseType ExerciseType { get; set; }
}
