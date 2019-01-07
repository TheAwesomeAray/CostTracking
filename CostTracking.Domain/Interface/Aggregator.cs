using System;
using System.Collections.Generic;

namespace CostTracking.Domain.Interface
{
    public interface DateAggregator
    {
        Dictionary<DateTime, decimal> Aggregate(IEnumerable<CostForDate> costsForDates);
    }
}
