using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetStats.Core.DTOs;
public class TrainingProgramDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateTrainingProgramDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}

public class UpdateTrainingProgramDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}
