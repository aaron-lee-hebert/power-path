using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SetStats.Core.Entities;
using SetStats.Core.Interfaces.Repositories;

namespace SetStats.Data.Repositories;
public class TrainingProgramRepository(ApplicationDbContext context) : Repository<TrainingProgram>(context), ITrainingProgramRepository
{

}
