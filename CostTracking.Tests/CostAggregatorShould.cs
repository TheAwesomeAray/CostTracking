using CostTracking.Domain;
using CostTracking.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CostTracking.Tests
{
    public class CostAggregatorShould
    {
        private Outage outage;

        public CostAggregatorShould()
        {
            outage = new Outage(DateTime.Parse("1/1/2018"), DateTime.Parse("1/30/2018"));
        }

        [Fact]
        public void AggregateCostsByDay()
        {
            var dateRangeForCosts = new List<DateTime> { DateTime.Parse("1/1/2018"), DateTime.Parse("1/2/2018"), DateTime.Parse("1/4/2018") };
            var costsForDates = GenerateCostsForDates(dateRangeForCosts);
            var costAggregator = AggregationFactory.GetAggregator(outage, AggregationMode.Daily);

            var aggregatedCosts = costAggregator.Aggregate(costsForDates);
            foreach (var date in dateRangeForCosts)
            {
                Assert.Equal(costsForDates.Where(x => x.Date == date).Sum(x => x.Cost), aggregatedCosts[date]);
            }
        }

        private List<CostForDate> GenerateCostsForDates(List<DateTime> dates)
        {
            int min = -500;
            int max = 500;
            var costsForDates = new List<CostForDate>();

            foreach (var date in dates)
            {
                costsForDates.Add(new CostForDate(new Random().Next(min, max), date));
            }

            return costsForDates;
        }
    }
}
