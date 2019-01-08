using CostTracking.Domain.Interface;
using System.Collections.Generic;
using System.Linq;

namespace CostTracking.Domain.Services
{
    public class TimeEntryAggregator
    {
        public Dictionary<string, decimal> Aggregate(IEnumerable<TimeEntry> timeEntries)
        {
            var names = timeEntries.Select(x => x.FullName).Distinct();
            var timeEntryGrouping = new Dictionary<string, decimal>();
            foreach (var name in names)
            {
                timeEntryGrouping.Add(name, timeEntries.Where(x => x.FullName == name).Sum(x => x.HoursWorked));
            }
            
            return timeEntryGrouping;
        }
    }
}
