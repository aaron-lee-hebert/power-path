namespace SetStats.Core.Entities;

public class TrainingCycle
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required int CycleNumber { get; set; }
    public required DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal RoundingFactor { get; set; } = 2.5M;
    public string? Notes { get; set; }

    public User? User { get; set; }
}
