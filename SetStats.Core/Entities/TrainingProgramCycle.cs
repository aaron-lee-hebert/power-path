using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SetStats.Core.AppEnums;

namespace SetStats.Core.Entities;
public class TrainingProgramCycle
{
    public Guid Id { get; set; }
    public Guid TrainingProgramId { get; set; }
    public Guid TrainingCycleId { get; set; }
    public string? Name { get; set; }
    public CycleSequence Sequence { get; set; }

    public bool IsActive { get; set; } = true;
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public required TrainingProgram TrainingProgram { get; set; }
    public required TrainingCycle TrainingCycle { get; set; }
}
