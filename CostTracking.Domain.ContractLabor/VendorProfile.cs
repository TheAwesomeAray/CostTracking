using System;
using System.Collections.Generic;
using System.Linq;

namespace CostTracking.Domain.ContractLabor
{
    public class VendorProfile
    {
        public DayOfWeek WorkWeekStart { get; private set; }
        public int OvertimeStartPoint { get; private set; }
        public IEnumerable<DateTime> HolidaySchedule { get; private set; }

        public VendorProfile(DayOfWeek workWeekStart, int overtimeStartPoint)
        {
            WorkWeekStart = workWeekStart;
            OvertimeStartPoint = overtimeStartPoint;
            HolidaySchedule = new List<DateTime>();
        }

        public VendorProfile(DayOfWeek workWeekStart, int overtimeStartPoint, IEnumerable<DateTime> holidaySchedule)
        {
            WorkWeekStart = workWeekStart;
            OvertimeStartPoint = overtimeStartPoint;
            HolidaySchedule = holidaySchedule;
        }

        public bool IsHoliday(DateTime date)
        {
            return HolidaySchedule.Contains(date);
        }
    }
}
