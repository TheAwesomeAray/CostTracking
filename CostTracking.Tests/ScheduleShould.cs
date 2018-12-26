using CostTracking.Domain;
using CostTracking.Domain.ContractLabor.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace CostTracking.Tests
{
    public class ScheduleShould
    {
        [Fact]
        public void Calculate_equivalent_headcounts_during_outage()
        {
            var weekDayDate = DateTime.Parse("1/4/2018");
            var weekEndDate = DateTime.Parse("1/6/2018");
            var postOutageDate = DateTime.Parse("2/1/2018");

            var daysWorked = new List<DateTime>()
            {
               weekDayDate
            };
            int desiredHeadCount = 11;
            var hoursSchedule = Helper.GetHoursSchedule(8, 12, 10);
            var outage = new Outage(DateTime.Parse("1/1/2018"), DateTime.Parse("1/30/2018"));
            var headCountService = new HeadCountService(hoursSchedule);
            var timeEntries = Helper.GetTimeEntriesForDateRange(hoursSchedule, desiredHeadCount, daysWorked, outage, Helper.GetVendorClassification("Boilermaker", 45, 60));

            var equivalentHeadCounts = headCountService.GetEquivalentHeadCount(timeEntries, outage, hoursSchedule);

            Assert.Equal(desiredHeadCount, equivalentHeadCounts[weekDayDate]);
        }

        [Fact]
        public void Determine_scheduled_hours_for_date()
        {
            var weekDayDate = DateTime.Parse("1/4/2018");
            var weekEndDate = DateTime.Parse("1/6/2018");
            var postOutageDate = DateTime.Parse("2/1/2018");
            int outageWeekdaySchedule = 8;
            int outageWeekendSchedule = 12;
            int prePostOutageSchedule = 10;
            var schedule = Helper.GetHoursSchedule(outageWeekdaySchedule, outageWeekendSchedule, prePostOutageSchedule);
            var outage = new Outage(DateTime.Parse("1/1/2018"), DateTime.Parse("1/30/2018"));

            Assert.Equal(outageWeekdaySchedule, schedule.GetScheduledHoursForDate(outage, weekDayDate));
            Assert.Equal(outageWeekendSchedule, schedule.GetScheduledHoursForDate(outage, weekEndDate));
            Assert.Equal(prePostOutageSchedule, schedule.GetScheduledHoursForDate(outage, postOutageDate));
        }
    }
}
