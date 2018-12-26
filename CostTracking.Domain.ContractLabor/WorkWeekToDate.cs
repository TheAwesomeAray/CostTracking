using System;

namespace CostTracking.Domain.ContractLabor
{
    public class WorkWeekToDate
    {
        public decimal HoursWorkedWeekToDate { get; set; }
        public decimal ScheduledHoursForDate { get; set; }
        public DateTime Date { get; set; }
    }
}
