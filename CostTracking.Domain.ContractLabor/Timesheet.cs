using System;

namespace CostTracking.Domain.ContractLabor
{
    public class TimeEntry
    {
        public DateTime DateWorked { get; private set; }
        public decimal HoursWorked { get; private set; }

        public TimeEntry(DateTime dateWorked, decimal hoursWorked, VendorClassification classification)
        {
            DateWorked = dateWorked;
            HoursWorked = hoursWorked;
        }
    }
}
