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
            decimal hoursWorked = 0;

            foreach (var schedule in headCountSchedules)
            {
                foreach (var entry in schedule.HeadCountEntries.OrderBy(x => x.Date))
                {
                    if (IsNewWorkWeek(entry, hoursSchedule))
                    {
                        hoursWorked = 0;
                    }

                    var scheduledHours = hoursSchedule.GetScheduledHoursForDate(outage, entry.Date);
                    dailyLaborCosts.Add(entry.Date, GetLaborCost(entry.HeadCount, schedule.Classification.GetRate(hoursWorked, hoursSchedule), scheduledHours));
                    hoursWorked += scheduledHours;
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
