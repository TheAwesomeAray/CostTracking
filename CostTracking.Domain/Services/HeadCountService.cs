using System;
using System.Collections.Generic;
using System.Linq;

namespace CostTracking.Domain.Services
{
    public class HeadCountService
    {
        private HoursSchedule schedule;

        public HeadCountService(HoursSchedule schedule)
        {
            this.schedule = schedule;
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

        private decimal GetScheduledHoursForDay(DateTime date, Outage outage)
        {
            if (!outage.DuringOutage(date))
            {
                return schedule.PrePostOutageHours;
            }
            else if (DateService.IsWeekDay(date))
            {
                return schedule.WeekDayHours;
            }

            return schedule.WeekDayHours;
        }
    }
}
