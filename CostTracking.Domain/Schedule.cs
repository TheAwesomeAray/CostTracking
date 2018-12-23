using CostTracking.Domain.Services;
using System;

namespace CostTracking.Domain
{
    public class HoursSchedule
    {
        public decimal WeekDayHours { get; private set; }
        public decimal WeekEndHours { get; private set; }
        public decimal PrePostOutageHours { get; private set; }
        public DayOfWeek WorkWeekStart { get; private set; }
        public int OvertimeStartPoint { get; private set; }

        public HoursSchedule(int weekdayHours, int weekendHours, int prePostOutageHours, DayOfWeek workWeekStart, int overtimeStartPoint)
        {
            WeekDayHours = weekdayHours;
            WeekEndHours = weekendHours;
            PrePostOutageHours = prePostOutageHours;
            WorkWeekStart = workWeekStart;
            OvertimeStartPoint = overtimeStartPoint;
        }

        public decimal GetScheduleForDate(Outage outage, DateTime dateTime)
        {
            if (!outage.DuringOutage(dateTime)) return PrePostOutageHours;
            else if (DateService.IsWeekDay(dateTime)) return WeekDayHours;

            return WeekEndHours;
        }
    }
}
