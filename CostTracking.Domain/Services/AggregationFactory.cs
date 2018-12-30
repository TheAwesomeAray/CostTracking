using CostTracking.Domain.Interface;
using System;

namespace CostTracking.Domain.Services
{
    public class AggregationFactory
    {
        public static Aggregator GetAggregator(Outage outage, AggregationMode mode)
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
    }

    public enum AggregationMode
    {
        Daily, 
        PayPeriod,
        Outage
    }

}
