using System.ComponentModel.DataAnnotations;

namespace SetStats.Core.DTOs;
public class TrainingCycleDto
{
    public Guid Id { get; set; }
    public required int CycleNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal RoundingFactor { get; set; }
    public string? Notes { get; set; }
}

public class CreateTrainingCycleDto
{
    public required int CycleNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal RoundingFactor { get; set; }
    public string? Notes { get; set; }
}

public class UpdateTrainingCycleDto
{
    public Guid Id { get; set; }
    public required int CycleNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal RoundingFactor { get; set; }
    public string? Notes { get; set; }
}
