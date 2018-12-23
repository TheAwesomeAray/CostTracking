using CostTracking.Domain;
using System;
using System.Collections.Generic;

namespace CostTracking.Tests
{
    public  static class TimeEntryHelper
    {
        public static List<TimeEntry> GetTimeEntriesForDateRange(HoursSchedule schedule, int desiredHeadCount, List<DateTime> dateRange, Outage outage, Classification classification)
        {
            var timeEntries = new List<TimeEntry>();

            foreach (var date in dateRange)
            {
                timeEntries.AddRange(GenerateTimeEntriesForDay(date, desiredHeadCount, schedule, outage, classification));
            }

            return timeEntries;
        }

        public static List<TimeEntry> GetTimeEntriesForDateRange(HoursSchedule schedule, int desiredHeadCount, DateTime startDate, DateTime endDate, Outage outage, Classification classification)
        {
            var timeEntries = new List<TimeEntry>();

            for (var date = startDate; date.DayOfYear != endDate.DayOfYear + 1; startDate.AddDays(1))
            {
                timeEntries.AddRange(GenerateTimeEntriesForDay(date, desiredHeadCount, schedule, outage, classification));
            }

            return timeEntries;
        }

        private static List<TimeEntry> GenerateTimeEntriesForDay(DateTime date, int desiredHeadCount, HoursSchedule schedule, Outage outage, Classification classification)
        {
            var timeEntriesForDay = new List<TimeEntry>();

            for (int i = 0; i < desiredHeadCount; i++)
            {
                timeEntriesForDay.Add(new TimeEntry(date, schedule.GetScheduledHoursForDate(outage, date), classification));
            }

            return timeEntriesForDay;
        }

        public static Classification GetDefaultClassification()
        {
            return new Classification("Default", 45, 60);
        }
    }
}
