using CostTracking.Domain;
using CostTracking.Domain.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace CostTracking.Tests
{
    public class ContractLaborCostServiceShould
    {
        [Fact]
        public void Generate_correct_cost_for_straight_time_work_day_pre_or_post_outage()
        {
            var dateTimeForCost = DateTime.Parse("1/15/2018");
            var hoursSchedule = new HoursSchedule(8, 12, 10, DayOfWeek.Saturday, 40);
            var classification = new Classification("Boilermaker", 45, 60);
            var outage = new Outage(DateTime.Parse("2/1/2018"), DateTime.Parse("2/30/2018"));
            var headCountEntries = new List<HeadCountEntry>() { new HeadCountEntry(5, dateTimeForCost) };
            var headCountSchedules = new List<HeadCountSchedule>() { new HeadCountSchedule(headCountEntries, classification) };

            var contractLaborCostService = new ContractLaborCostService();
            var result = contractLaborCostService.GetProjectedCostsForDateRange(outage, hoursSchedule, headCountSchedules);

            Assert.Equal(450, result[dateTimeForCost]);
        }
    }
}
