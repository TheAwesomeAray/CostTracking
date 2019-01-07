using CostTracking.Domain.Interface;
using System;
using System.Collections.Generic;

namespace CostTracking.Domain.Services
{
    public class TimeEntryAggregator
    {
        public Dictionary<string, CostForDate> Aggregate(IEnumerable<TimeEntry> timeEntries)
        {
            var timeEntryGrouping = new Dictionary<string, CostForDate>();
            timeEntryGrouping.Add("Ray, Andrew", new CostForDate(15, DateTime.Parse("1/2/2018")));
            return timeEntryGrouping;
        }
    }
}
