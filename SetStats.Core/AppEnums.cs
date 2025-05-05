namespace SetStats.Core;

public static class AppEnums
{
    public enum WorkoutCategory
    {
        Main = 0,
        Assistance
    }

    public enum WeekType
    {
        Standard = 0,
        Deload
    }

    public enum WorkoutDayStatus
    {
        Planned = 0,
        Completed,
        Skipped
    }

    public enum CycleSequence
    {
        Intro = 0,
        Strength,
        Deload
    }
}
