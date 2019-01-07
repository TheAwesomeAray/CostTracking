using System;
using System.Collections.Generic;
using System.Linq;

namespace CostTracking.Domain.ContractLabor.Services
{
    public class HeadCountService
    {
        private HoursSchedule schedule;

        public HeadCountService(HoursSchedule schedule)
        {
            this.schedule = schedule;
        }

        public Dictionary<DateTime, decimal> GetEquivalentHeadCount(List<VendorTimeEntry> timeEntries, Outage outage, HoursSchedule hoursSchedule)
        {
            var dayGroups = timeEntries.GroupBy(t => t.DateWorked.Day);
            var equivalentHeadCounts = new Dictionary<DateTime, decimal>();

            foreach (var group in dayGroups)
            {
                decimal totalHoursWorkedForDay = group.Sum(x => x.STHoursWorked + x.OTHoursWorked);
                DateTime dateWorked = group.First().DateWorked;
                decimal equivalentHeadCount = totalHoursWorkedForDay / hoursSchedule.GetScheduledHoursForDate(outage, dateWorked);
                equivalentHeadCounts.Add(dateWorked, equivalentHeadCount);
            }

            return equivalentHeadCounts;
        }
    }
}
