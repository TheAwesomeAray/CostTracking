using CostTracking.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CostTracking.Domain.Services
{
    public class PayPeriodAggregationService : Aggregator
    {
        private DateTime PayPeriodStartDate { get; set; }
        private static int PayPeriodLength = 14;

        public PayPeriodAggregationService(DateTime payPeriodStartDate)
        {
            PayPeriodStartDate = payPeriodStartDate;
        }

        public Dictionary<DateTime, decimal> Aggregate(IEnumerable<CostForDate> costsForDates)
        {
            var payPeriodEndDate = GetStartingPayPeriodForCosts(costsForDates);
            var costsGroupedByPayPeriod = new Dictionary<DateTime, decimal>();
            var enumerator = costsForDates.OrderBy(x => x.Date).GetEnumerator();
            decimal runningTotal = 0;

            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Date > payPeriodEndDate)
                {
                    costsGroupedByPayPeriod.Add(payPeriodEndDate, runningTotal);
                    payPeriodEndDate = payPeriodEndDate.AddDays(PayPeriodLength);
                    runningTotal = 0;
                }

                runningTotal += enumerator.Current.Cost;
            }

            costsGroupedByPayPeriod.Add(payPeriodEndDate, runningTotal);

            return costsGroupedByPayPeriod;
        }

        private DateTime GetStartingPayPeriodForCosts(IEnumerable<CostForDate> costsForDate)
        {
            var startingPoint = PayPeriodStartDate;
            while (costsForDate.Select(x => x.Date)
                               .Where(d => d < startingPoint.AddDays(-PayPeriodLength)).Any())
            {
                startingPoint = startingPoint.AddDays(-PayPeriodLength);
            }

            return startingPoint;
        }
    }
}
