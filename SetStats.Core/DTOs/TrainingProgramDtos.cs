using System.ComponentModel.DataAnnotations;

namespace SetStats.Core.DTOs;
public class TrainingProgramDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    [Display(Name = "Is Active")]
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateTrainingProgramDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    [Display(Name = "Is Active")]
    public bool IsActive { get; set; } = true;
}

public class UpdateTrainingProgramDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    [Display(Name = "Is Active")]
    public bool IsActive { get; set; }
}
