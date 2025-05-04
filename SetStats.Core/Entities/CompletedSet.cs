using System.ComponentModel.DataAnnotations;

namespace SetStats.Core.Entities;

public class CompletedSet
{
    public Guid Id { get; set; }
    public required Guid ProgrammedSetId { get; set; }

    public required decimal ActualWeight { get; set; }
    public required int ActualReps { get; set; }
    [Range(1, 10)]
    public int? RateOfPerceivedExertion { get; set; }

    public string? Notes { get; set; }

    public required ProgrammedSet ProgrammedSet { get; set; }
}
