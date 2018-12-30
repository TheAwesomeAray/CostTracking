using CostTracking.Domain;
using CostTracking.Domain.Commands;
using CostTracking.Domain.ContractLabor;
using CostTracking.Domain.ContractLabor.Services;
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
        private CompanyClassification companyClassification;
        private HoursSchedule hoursSchedule;

        public CostAggregatorShould()
        {
            var command = new OutageCreateCommand()
            {
                OutageStartDate = DateTime.Parse("1/1/2018"),
                OutageEndDate = DateTime.Parse("1/30/2018"),
                PayPeriodStartDate = DateTime.Parse("2/12/2018")
            };
            outage = new Outage(command);
            companyClassification = Helper.GetCompanyClassification("Boilermaker", 45, false);
            hoursSchedule = Helper.GetHoursSchedule(8, 12, 10);
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

        [Fact]
        public void GroupCostsByPayPeriod()
        {
            var companyClassification = Helper.GetCompanyClassification("test", 45, true);
            var headCountSchedules = Helper.CreateHeadCountSchedule(10, DateTime.Parse("2/11/2018"), 4, companyClassification);
            var projectedCosts = new ProjectedLaborCostService(outage).GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules)
                                                                      .Select(x => new CostForDate(x.Value, x.Key));

            var result = AggregationFactory.GetAggregator(outage, AggregationMode.PayPeriod).Aggregate(projectedCosts);

            Assert.Equal(9000, result[outage.PayPeriodStartDate]);
            Assert.Equal(15750, result[outage.PayPeriodStartDate.AddDays(14)]);
        }

        [Fact]
        public void ShiftPayPeriodIfValuesFallPriorToFirstPayPeriod()
        {
            var companyClassification = Helper.GetCompanyClassification("test", 45, true);
            var headCountSchedules = Helper.CreateHeadCountSchedule(10, DateTime.Parse("2/11/2018"), 4, companyClassification);
            var projectedCosts = new ProjectedLaborCostService(outage).GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules)
                                                                      .Select(x => new CostForDate(x.Value, x.Key));
            var localOutage = new Outage(new OutageCreateCommand()
            {
                OutageStartDate = DateTime.Parse("1/1/2018"),
                OutageEndDate = DateTime.Parse("1/30/2018"),
                PayPeriodStartDate = DateTime.Parse("2/26/2018")
            });

            var result = AggregationFactory.GetAggregator(localOutage, AggregationMode.PayPeriod).Aggregate(projectedCosts);

            Assert.Equal(9000, result[localOutage.PayPeriodStartDate.AddDays(-14)]);
            Assert.Equal(15750, result[localOutage.PayPeriodStartDate]);
        }
    }
}
