using CostTracking.Domain;
using CostTracking.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CostTracking.Tests
{
    public class ScheduleShould
    {
        [Fact]
        public void Calculate_equivalent_headcounts_during_outage()
        {
            var firstTimeEntryDay = DateTime.Parse("1/4/2018");
            var secondTimeEntryDay = firstTimeEntryDay.AddDays(1);
            var timeEntries = new List<TimeEntry>()
            {
                new TimeEntry(firstTimeEntryDay, 8),
                new TimeEntry(firstTimeEntryDay, 8),
                new TimeEntry(firstTimeEntryDay, 8),
                new TimeEntry(firstTimeEntryDay, 8),
                new TimeEntry(firstTimeEntryDay, 8)
            };
            var schedule = new Schedule(8, 8, 8, DayOfWeek.Saturday);
            var outage = new Outage(DateTime.Parse("1/1/2018"), DateTime.Parse("1/30/2018"));

            var equivalentHeadCounts = schedule.GetEquivalentHeadCount(timeEntries, outage);

            Assert.True(equivalentHeadCounts[firstTimeEntryDay] == 5);
        }
    }
}
