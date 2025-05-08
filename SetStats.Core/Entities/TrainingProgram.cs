namespace SetStats.Core.Entities;
public class TrainingProgram
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Goal { get; set; }

    public bool IsActive { get; set; } = true;
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User? User { get; set; }
}
