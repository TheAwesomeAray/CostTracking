using System;
using System.Collections.Generic;
using System.Linq;

namespace CostTracking.Domain.ContractLabor.Services
{
    public class ProjectedLaborCostService
    {
        private Outage outage;

        public ProjectedLaborCostService(Outage outage)
        {
            this.outage = outage;
        }

        public Dictionary<DateTime, decimal> GetProjectedCostsForDateRange(HoursSchedule hoursSchedule, List<HeadCountSchedule> headCountSchedules)
        {
            var dailyLaborCosts = new Dictionary<DateTime, decimal>();
            var workWeek = new WorkWeekToDate();

            foreach (var schedule in headCountSchedules)
            {
                foreach (var entry in schedule.HeadCountEntries.OrderBy(x => x.Date))
                {
                    workWeek.Date = entry.Date;

                    if (hoursSchedule.IsNewWorkWeek(entry))
                        workWeek.HoursWorkedWeekToDate = 0;

                    workWeek.ScheduledHoursForDate = hoursSchedule.GetScheduledHoursForDate(outage, entry.Date);
                    var rate = schedule.Classification.GetRate(workWeek);
                    dailyLaborCosts.Add(entry.Date, GetLaborCost(entry.HeadCount, rate, workWeek.ScheduledHoursForDate));

                    workWeek.HoursWorkedWeekToDate += workWeek.ScheduledHoursForDate;
                }
            }

            return dailyLaborCosts;
        }
        
        private decimal GetLaborCost(int headCount, decimal rate, decimal hours)
        {
            return headCount * hours * rate;
        }
    }
}
