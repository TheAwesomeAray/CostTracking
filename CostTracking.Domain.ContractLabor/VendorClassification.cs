using System;

namespace CostTracking.Domain.ContractLabor
{
    public class VendorClassification : Classification
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        private decimal HolidayRate { get; set; }

        public VendorClassification(string name, decimal straightTimeRate, decimal overtimeRate, int vendorProfileId, int holidayRate = 0) : base(vendorProfileId)
        {
            Name = name;
            StraightTimeRate = straightTimeRate;
            OvertimeRate = overtimeRate;
            HolidayRate = holidayRate;
        }

        public override decimal GetRate(WorkWeekToDate workWeek)
        {
            if (VendorProfile.IsHoliday(workWeek.Date))
                return HolidayRate;

            return base.GetRate(workWeek);
        }
    }
}