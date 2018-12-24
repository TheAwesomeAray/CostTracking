using System;

namespace CostTracking.Domain.ContractLabor
{
    public class VendorProfile
    {
        public DayOfWeek WorkWeekStart { get; private set; }
        public int OvertimeStartPoint { get; private set; }

        public VendorProfile(DayOfWeek workWeekStart, int overtimeStartPoint)
        {
            WorkWeekStart = workWeekStart;
            OvertimeStartPoint = overtimeStartPoint;
        }
    }
}
