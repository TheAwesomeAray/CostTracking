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

        public static VendorClassification GetVendorClassification(string name, decimal straightTimeRate, decimal overtimerate)
        {
            return ApplyVendorProfileToPrivateField(new VendorClassification(name, straightTimeRate, overtimerate, 1), GetDefaultVendorProfile()) as VendorClassification;
        }

        public static VendorClassification GetVendorClassificationWithHoliday(decimal holidayRate, DateTime holiday)
        {
            return ApplyVendorProfileToPrivateField(new VendorClassification("HolidayVendor", 0, 0, 1, 90), 
                                new VendorProfile(DayOfWeek.Sunday, 40 , new List<DateTime>() { holiday })) as VendorClassification;
        }

        public static CompanyClassification GetCompanyClassification(string name, decimal straightTimeRate, bool exempt)
        {
            return ApplyVendorProfileToPrivateField(new CompanyClassification(name, straightTimeRate, 1, exempt), GetDefaultVendorProfile()) as CompanyClassification;
        }

        public static HoursSchedule GetHoursSchedule(int weekDayHours, int weekEndHours, int prePostOutageHours)
        {
            return ApplyVendorProfileToPrivateField(new HoursSchedule(weekDayHours, weekEndHours, prePostOutageHours, 1), GetDefaultVendorProfile()) as HoursSchedule;
        }

        

        public static object ApplyVendorProfileToPrivateField(object hasVendorProfile, VendorProfile vendorProfile)
        {
            var type = hasVendorProfile.GetType();
            
            var prop = type.GetProperty("VendorProfile", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (prop == null)
            {
                type = type.BaseType;
                prop = type.GetProperty("VendorProfile", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            }
            
            prop.SetValue(hasVendorProfile, vendorProfile);
            
            return hasVendorProfile;
        }

        private static VendorProfile GetDefaultVendorProfile()
        {
            return new VendorProfile(DayOfWeek.Sunday, 40);
        }
    }
}
