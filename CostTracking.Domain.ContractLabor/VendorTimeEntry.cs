using CostTracking.Domain.Interface;
using System;

namespace CostTracking.Domain.ContractLabor
{
    public class VendorTimeEntry : TimeEntry
    {
        public DateTime DateWorked { get; private set; }
        public decimal STHoursWorked { get; private set; }
        public decimal OTHoursWorked { get; private set; }

        public VendorTimeEntry(DateTime dateWorked, decimal stHoursWorked, VendorClassification classification)
        {
            DateWorked = dateWorked;
            STHoursWorked = stHoursWorked;
        }
    }
}
