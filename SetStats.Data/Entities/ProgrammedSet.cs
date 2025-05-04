namespace SetStats.Data.Entities;

public class ProgrammedSet
{
    public Guid Id { get; set; }
    public required Guid WorkoutDayId { get; set; }
    public required Guid ExerciseTypeId { get; set; }

    public required decimal PercentageOfTrainingMax { get; set; }
    public required int TargetReps { get; set; }
    public required int SetOrder { get; set; }

    public required bool IsAMRAP { get; set; }

    public required WorkoutDay WorkoutDay { get; set; }
    public required ExerciseType ExerciseType { get; set; }
}
