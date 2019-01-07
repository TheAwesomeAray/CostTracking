using CostTracking.Domain.Interface;
using System;
using System.Collections.Generic;

namespace CostTracking.Domain.Services
{
    public class AggregationFactory
    {
        public static DateAggregator GetAggregator(Outage outage, AggregationMode mode)
        {
            switch (mode)
            {
                case AggregationMode.Daily:
                    return new DailyAggregationService();
                case AggregationMode.PayPeriod:
                    return new PayPeriodAggregationService(outage.PayPeriodStartDate);
                default:
                    throw new InvalidOperationException("Invalid Aggregation Mode.");
            }
        }

        public static TimeEntryAggregator GetIndividualAggregator(Outage outage, List<TimeEntry> list)
        {
            return new TimeEntryAggregator();
        }
    }

    public enum AggregationMode
    {
        Daily, 
        PayPeriod,
        Outage
    }

}
