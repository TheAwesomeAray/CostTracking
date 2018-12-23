using System;
using System.Collections.Generic;
using System.Linq;

namespace CostTracking.Domain.Services
{
    public class ContractLaborCostService
    {
        private Outage outage;

        public ContractLaborCostService(Outage outage)
        {
            this.outage = outage;
        }

        public Dictionary<DateTime, decimal> GetProjectedCostsForDateRange(HoursSchedule hoursSchedule, List<HeadCountSchedule> headCountSchedules)
        {
            var dailyLaborCosts = new Dictionary<DateTime, decimal>();

            foreach (var schedule in headCountSchedules)
            {
                foreach (var entry in schedule.HeadCountEntries.OrderBy(x => x.Date))
                {
                    dailyLaborCosts.Add(entry.Date, GetLaborCost(entry.HeadCount, schedule.Classification.StraightTimeRate, hoursSchedule.PrePostOutageHours));
                }
            }

            return dailyLaborCosts;
        }

        private decimal GetLaborCost(int headCount, decimal rate, decimal hours)
        {
            return headCount * hours * rate;
        }

        private bool IsNewWorkWeek(HeadCountEntry entry, HoursSchedule hoursSchedule)
        {
            return entry.Date.DayOfWeek == hoursSchedule.WorkWeekStart;
        }
    }
}
