using Microsoft.Extensions.DependencyInjection;
using SetStats.Core.Interfaces.Repositories;
using SetStats.Core.Interfaces.Services;
using SetStats.Data.Repositories;

namespace SetStats.Services;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        _ = services.AddScoped<ITrainingProgramService, TrainingProgramService>();
        // Add more services here
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        _ = services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        _ = services.AddScoped<ICompletedSetRepository, CompletedSetRepository>();
        _ = services.AddScoped<IExerciseTypeRepository, ExerciseTypeRepository>();
        _ = services.AddScoped<IProgrammedSetRepository, ProgrammedSetRepository>();
        _ = services.AddScoped<IProgressRecordRepository, ProgressRecordRepository>();
        _ = services.AddScoped<ITrainingCycleRepository, TrainingCycleRepository>();
        _ = services.AddScoped<ITrainingProgramCycleRepository, TrainingProgramCycleRepository>();
        _ = services.AddScoped<ITrainingProgramRepository, TrainingProgramRepository>();
        _ = services.AddScoped<IUserMaxRepository, UserMaxRepository>();
        _ = services.AddScoped<IWorkoutDayRepository, WorkoutDayRepository>();
        _ = services.AddScoped<IWorkoutWeekRepository, WorkoutWeekRepository>();
        return services;
    }
}
