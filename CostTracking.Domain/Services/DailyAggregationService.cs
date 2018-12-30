using CostTracking.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CostTracking.Domain.Services
{
    public class DailyAggregationService : Aggregator
    {
        public Dictionary<DateTime, decimal> Aggregate(IEnumerable<CostForDate> costsForDates)
        {
            var dailyLaborCosts = new Dictionary<DateTime, decimal>();
            var dates = costsForDates.Select(x => x.Date).Distinct();
            foreach (var date in dates)
            {
                dailyLaborCosts.Add(date, GetCostForDay(date, costsForDates));
            }

            return dailyLaborCosts;
        }

        private decimal GetCostForDay(DateTime date, IEnumerable<CostForDate> costsForDates)
        {
            return costsForDates.Where(x => x.Date == date).Sum(x => x.Cost);
        }
    }
}
