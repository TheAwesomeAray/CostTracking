using System;
using System.Collections.Generic;
using System.Linq;

namespace CostTracking.Domain
{
    public class Schedule
    {
        private int WeekDayHours { get; set; }
        private int WeekEndHours { get; set; }
        private int PrePostOutageHours { get; set; }
        private DayOfWeek WorkWeekStart { get; set; }

        public Schedule(int weekdayHours, int weekendHours, int prePostOutageHours, DayOfWeek workWeekStart)
        {
            WeekDayHours = weekdayHours;
            WeekEndHours = weekendHours;
            PrePostOutageHours = PrePostOutageHours;
            WorkWeekStart = workWeekStart;
        }

        public int GetEquivalentHeadCount(List<TimeEntry> timeEntries)
        {
            var dayGroups = timeEntries.GroupBy(t => t.DateWorked.Day);
            return 1;
        }
    }
}
