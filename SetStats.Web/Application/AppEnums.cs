namespace SetStats.Web.Application;

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
}
