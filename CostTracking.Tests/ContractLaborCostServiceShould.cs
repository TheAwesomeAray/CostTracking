using CostTracking.Domain;
using CostTracking.Domain.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace CostTracking.Tests
{
    public class ContractLaborCostServiceShould
    {
        private Outage outage;
        private Classification classification;
        private HoursSchedule hoursSchedule;

        public ContractLaborCostServiceShould()
        {
            outage = new Outage(DateTime.Parse("2/1/2018"), DateTime.Parse("2/28/2018"));
            classification = new Classification("Boilermaker", 45, 60);
            hoursSchedule = new HoursSchedule(8, 12, 10, DayOfWeek.Sunday, 40);
        }

        [Fact]
        public void Generate_correct_cost_for_straight_time_pre_or_post_outage()
        {
            var dateTimeForCost = DateTime.Parse("1/15/2018");
            var headCountEntries = new List<HeadCountEntry>() { new HeadCountEntry(10, dateTimeForCost) };
            var headCountSchedules = new List<HeadCountSchedule>() { new HeadCountSchedule(headCountEntries, classification) };

            var contractLaborCostService = new ContractLaborCostService(outage);
            var result = contractLaborCostService.GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules);

            Assert.Equal(4500, result[dateTimeForCost]);
        }

        [Fact]
        public void Generate_correct_cost_for_straight_time_during_outage_weekday()
        {
            var dateTimeForCost = DateTime.Parse("2/15/2018");
            var headCountEntries = new List<HeadCountEntry>() { new HeadCountEntry(10, dateTimeForCost) };
            var headCountSchedules = new List<HeadCountSchedule>() { new HeadCountSchedule(headCountEntries, classification) };

            var contractLaborCostService = new ContractLaborCostService(outage);
            var result = contractLaborCostService.GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules);

            Assert.Equal(3600, result[dateTimeForCost]);
        }

        [Fact]
        public void Generate_correct_cost_for_overtime_during_outage_weekday()
        {
            var dateTimeForCost = DateTime.Parse("2/12/2018");
            var dateTimeForOvertime = dateTimeForCost.AddDays(5);
            var headCountEntries = new List<HeadCountEntry>() {
                new HeadCountEntry(10, dateTimeForCost),
                new HeadCountEntry(10, dateTimeForCost.AddDays(1)),
                new HeadCountEntry(10, dateTimeForCost.AddDays(2)),
                new HeadCountEntry(10, dateTimeForCost.AddDays(3)),
                new HeadCountEntry(10, dateTimeForCost.AddDays(4)),
                new HeadCountEntry(10, dateTimeForOvertime),
            };
            var headCountSchedules = new List<HeadCountSchedule>() { new HeadCountSchedule(headCountEntries, classification) };

            var contractLaborCostService = new ContractLaborCostService(outage);
            var result = contractLaborCostService.GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules);

            Assert.Equal(7200, result[dateTimeForOvertime]);
        }

        [Fact]
        public void Generate_correct_cost_for_partial_straighttime_and_partial_overtime()
        {
            var dateTimeForCost = DateTime.Parse("2/11/2018");
            var dateTimeForOvertime = dateTimeForCost.AddDays(4);
            var headCountEntries = new List<HeadCountEntry>() {
                new HeadCountEntry(10, dateTimeForCost),
                new HeadCountEntry(10, dateTimeForCost.AddDays(1)),
                new HeadCountEntry(10, dateTimeForCost.AddDays(2)),
                new HeadCountEntry(10, dateTimeForCost.AddDays(3)),
                new HeadCountEntry(10, dateTimeForOvertime),
            };
            var headCountSchedules = new List<HeadCountSchedule>() { new HeadCountSchedule(headCountEntries, classification) };

            var contractLaborCostService = new ContractLaborCostService(outage);
            var result = contractLaborCostService.GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules);

            Assert.Equal(4200, result[dateTimeForOvertime]);
        }
    }
}
