using CostTracking.Domain.Interface;

namespace CostTracking.Domain.Services
{
    public class AggregationFactory
    {
        public static Aggregator GetAggregator(Outage otuage, AggregationMode mode)
        {
            return new DailyAggregationService();
        }
    }

    public enum AggregationMode
    {
        Daily, 
        PayPeriod,
        Outage
    }

}
