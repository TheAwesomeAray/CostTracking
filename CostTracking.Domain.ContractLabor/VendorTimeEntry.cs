using CostTracking.Domain.Interface;
using System;

namespace CostTracking.Domain.ContractLabor
{
    public class VendorTimeEntry : TimeEntry
    {
        public DateTime DateWorked { get; private set; }
        public decimal STHoursWorked { get; private set; }
        public decimal OTHoursWorked { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName => $"{LastName}, {FirstName}";
        public decimal HoursWorked => STHoursWorked + OTHoursWorked;

        public VendorTimeEntry(DateTime dateWorked, string firstName, string lastName, decimal stHoursWorked, VendorClassification classification)
        {
            DateWorked = dateWorked;
            STHoursWorked = stHoursWorked;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
