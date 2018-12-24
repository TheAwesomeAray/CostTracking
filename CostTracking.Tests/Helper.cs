using CostTracking.Domain;
using CostTracking.Domain.ContractLabor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CostTracking.Tests
{
    public  static class Helper
    {
        public static List<TimeEntry> GetTimeEntriesForDateRange(HoursSchedule schedule, int desiredHeadCount, List<DateTime> dateRange, Outage outage, VendorClassification classification)
        {
            var timeEntries = new List<TimeEntry>();

            foreach (var date in dateRange)
            {
                timeEntries.AddRange(GenerateTimeEntriesForDay(date, desiredHeadCount, schedule, outage, classification));
            }

            return timeEntries;
        }

        public static List<TimeEntry> GetTimeEntriesForDateRange(HoursSchedule schedule, int desiredHeadCount, DateTime startDate, DateTime endDate, Outage outage, VendorClassification classification)
        {
            var timeEntries = new List<TimeEntry>();

            for (var date = startDate; date.DayOfYear != endDate.DayOfYear + 1; startDate.AddDays(1))
            {
                timeEntries.AddRange(GenerateTimeEntriesForDay(date, desiredHeadCount, schedule, outage, classification));
            }

            return timeEntries;
        }

        private static List<TimeEntry> GenerateTimeEntriesForDay(DateTime date, int desiredHeadCount, HoursSchedule schedule, Outage outage, VendorClassification classification)
        {
            var timeEntriesForDay = new List<TimeEntry>();

            for (int i = 0; i < desiredHeadCount; i++)
            {
                timeEntriesForDay.Add(new TimeEntry(date, schedule.GetScheduledHoursForDate(outage, date), classification));
            }

            return timeEntriesForDay;
        }

        public static VendorClassification GetClassification(string name, decimal straightTimeRate, decimal overtimerate)
        {
            return ApplyVendorProfileToPrivateField(new VendorClassification(name, straightTimeRate, overtimerate, 1)) as VendorClassification;
        }

        public static HoursSchedule GetHoursSchedule(int weekDayHours, int weekEndHours, int prePostOutageHours)
        {
            return ApplyVendorProfileToPrivateField(new HoursSchedule(weekDayHours, weekEndHours, prePostOutageHours, 1)) as HoursSchedule;
        }

        public static object ApplyVendorProfileToPrivateField(object hasVendorProfile)
        {
            var type = hasVendorProfile.GetType();
            var fields = type.GetProperties().ToList();
            var prop = type.GetProperty("VendorProfile", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            prop.SetValue(hasVendorProfile, new VendorProfile(DayOfWeek.Sunday, 40));
            return hasVendorProfile;
        }
    }
}
