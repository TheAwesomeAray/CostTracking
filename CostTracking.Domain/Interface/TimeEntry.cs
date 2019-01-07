namespace CostTracking.Domain.Interface
{
    public interface TimeEntry
    {
        decimal HoursWorked { get; }
        string FullName { get; }
    }
}
