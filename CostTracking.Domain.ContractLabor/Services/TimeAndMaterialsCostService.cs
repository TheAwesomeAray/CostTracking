using System;
using System.Collections.Generic;
using System.Linq;

namespace CostTracking.Domain.ContractLabor.Services
{
    public class TimeAndMaterialsService
    {
        private Outage outage;

        public TimeAndMaterialsService(Outage outage)
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
                    if (hoursSchedule.IsNewWorkWeek(entry))
                        hoursWorked = 0;

                    var scheduledHours = hoursSchedule.GetScheduledHoursForDate(outage, entry.Date);
                    dailyLaborCosts.Add(entry.Date, GetLaborCost(entry.HeadCount, schedule.Classification.GetRate(hoursWorked, scheduledHours), scheduledHours));
                    hoursWorked += scheduledHours;
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
