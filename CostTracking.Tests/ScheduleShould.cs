using CostTracking.Domain;
using CostTracking.Domain.Services;
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
            var schedule = new HoursSchedule(8, 12, 10, DayOfWeek.Saturday, 40);
            var outage = new Outage(DateTime.Parse("1/1/2018"), DateTime.Parse("1/30/2018"));
            var headCountService = new HeadCountService(schedule);
            var timeEntries = TimeEntryHelper.GetTimeEntriesForDateRange(schedule, desiredHeadCount, daysWorked, outage, TimeEntryHelper.GetDefaultClassification());

            var equivalentHeadCounts = headCountService.GetEquivalentHeadCount(timeEntries, outage);

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
            var schedule = new HoursSchedule(outageWeekdaySchedule, outageWeekendSchedule, prePostOutageSchedule, DayOfWeek.Saturday, 40);
            var outage = new Outage(DateTime.Parse("1/1/2018"), DateTime.Parse("1/30/2018"));

            Assert.Equal(outageWeekdaySchedule, schedule.GetScheduledHoursForDate(outage, weekDayDate));
            Assert.Equal(outageWeekendSchedule, schedule.GetScheduledHoursForDate(outage, weekEndDate));
            Assert.Equal(prePostOutageSchedule, schedule.GetScheduledHoursForDate(outage, postOutageDate));
        }
    }
}
