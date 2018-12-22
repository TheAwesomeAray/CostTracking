using CostTracking.Domain.Services;
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

        public Dictionary<DateTime, decimal> GetEquivalentHeadCount(List<TimeEntry> timeEntries, Outage outage)
        {
            var dayGroups = timeEntries.GroupBy(t => t.DateWorked.Day);
            var equivalentHeadCounts = new Dictionary<DateTime, decimal>();

            foreach (var group in dayGroups)
            {
                decimal totalHoursWorkedForDay = group.Sum(x => x.HoursWorked);
                DateTime dateWorked = group.First().DateWorked;
                decimal equivalentHeadCount = totalHoursWorkedForDay / GetScheduledHoursForDay(dateWorked, outage);
                equivalentHeadCounts.Add(dateWorked, equivalentHeadCount);
            }

            return equivalentHeadCounts;
        }

        private int GetScheduledHoursForDay(DateTime date, Outage outage)
        {
            if (!outage.DuringOutage(date))
            {
                return PrePostOutageHours;
            }
            else if (DateService.IsWeekDay(date))
            {
                return WeekDayHours;
            }

            return WeekDayHours;
        }
    }
}
