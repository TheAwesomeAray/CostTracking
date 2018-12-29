using System;
using System.Collections.Generic;
using System.Linq;

namespace CostTracking.Domain.ContractLabor.Services
{
    public class PayPeriodService
    {
        private DateTime PayPeriodStartDate { get; set; }
        private static int TwoWeeks = 14;

        public PayPeriodService(DateTime payPeriodStartDate)
        {
            PayPeriodStartDate = payPeriodStartDate;
        }

        public Dictionary<DateTime, decimal> GroupCostsByPayPeriod(Dictionary<DateTime, decimal> costsForDate)
        {
            var payPeriodEndDate = GetStartingPayPeriodForCosts(costsForDate);
            var costsGroupedByPayPeriod = new Dictionary<DateTime, decimal>();
            int count = costsForDate.Count;

            while (costsForDate.Any())
            {
                var groupedCost = costsForDate.Where(x => x.Key < payPeriodEndDate);
                costsGroupedByPayPeriod.Add(payPeriodEndDate, groupedCost.Sum(x => x.Value));
                RemoveGroupedCosts(costsForDate, groupedCost.ToList());
                payPeriodEndDate = payPeriodEndDate.AddDays(TwoWeeks);
            }

            return costsGroupedByPayPeriod;
        }

        private void RemoveGroupedCosts(Dictionary<DateTime, decimal> allCosts, List<KeyValuePair<DateTime, decimal>> usedCosts)
        {
            for (int i = 0; i < usedCosts.Count(); i++)
            {
                allCosts.Remove(usedCosts[i].Key);
            }
        }

        private DateTime GetStartingPayPeriodForCosts(Dictionary<DateTime, decimal> costsForDate)
        {
            var startingPoint = PayPeriodStartDate;

            while (costsForDate.Keys.Where(d => d < startingPoint.AddDays(-TwoWeeks)).Any())
            {
                startingPoint = startingPoint.AddDays(-TwoWeeks);
            }

            return startingPoint;
        }
    }
}
