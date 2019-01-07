namespace CostTracking.Domain.Interface
{
    public interface TimeEntry
    {
        decimal STHoursWorked { get; }
        decimal OTHoursWorked { get; }
    }
}
