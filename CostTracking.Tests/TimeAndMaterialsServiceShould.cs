using CostTracking.Domain;
using CostTracking.Domain.ContractLabor;
using CostTracking.Domain.ContractLabor.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace CostTracking.Tests
{
    public class TimeAndMaterialsServiceShould
    {
        private Outage outage;
        private VendorClassification classification;
        private HoursSchedule hoursSchedule;

        public TimeAndMaterialsServiceShould()
        {
            outage = new Outage(DateTime.Parse("2/1/2018"), DateTime.Parse("2/28/2018"));
            classification = Helper.GetVendorClassification("Boilermaker", 45, 60);
            hoursSchedule = Helper.GetHoursSchedule(8, 12, 10);
        }

        [Fact]
        public void Generate_correct_cost_for_straight_time_pre_or_post_outage()
        {
            var dateTimeForCost = DateTime.Parse("1/15/2018");
            var headCountEntries = new List<HeadCountEntry>() { new HeadCountEntry(10, dateTimeForCost) };
            var headCountSchedules = new List<HeadCountSchedule>() { new HeadCountSchedule(headCountEntries, classification) };

            var timeAndMaterialsService = new TimeAndMaterialsService(outage);
            var result = timeAndMaterialsService.GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules);

            Assert.Equal(4500, result[dateTimeForCost]);
        }

        [Fact]
        public void Generate_correct_cost_for_straight_time_during_outage_weekday()
        {
            var dateTimeForCost = DateTime.Parse("2/15/2018");
            var headCountEntries = new List<HeadCountEntry>() { new HeadCountEntry(10, dateTimeForCost) };
            var headCountSchedules = new List<HeadCountSchedule>() { new HeadCountSchedule(headCountEntries, classification) };

            var timeAndMaterialsService = new TimeAndMaterialsService(outage);
            var result = timeAndMaterialsService.GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules);

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

            var timeAndMaterialsService = new TimeAndMaterialsService(outage);
            var result = timeAndMaterialsService.GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules);

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

            var timeAndMaterialsService = new TimeAndMaterialsService(outage);
            var result = timeAndMaterialsService.GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules);

            Assert.Equal(4200, result[dateTimeForOvertime]);
        }

        [Fact]
        public void UseStraightTimeRateForExemptCompanyClassifications()
        {
            var companyClassification = Helper.GetCompanyClassification("test", 45, true);
            var dateTimeForCost = DateTime.Parse("2/11/2018");
            var dateTimeForOvertime = dateTimeForCost.AddDays(4);
            var headCountEntries = new List<HeadCountEntry>() {
                new HeadCountEntry(10, dateTimeForCost),
                new HeadCountEntry(10, dateTimeForCost.AddDays(1)),
                new HeadCountEntry(10, dateTimeForCost.AddDays(2)),
                new HeadCountEntry(10, dateTimeForCost.AddDays(3)),
                new HeadCountEntry(10, dateTimeForOvertime),
            };
            var headCountSchedules = new List<HeadCountSchedule>() { new HeadCountSchedule(headCountEntries, companyClassification) };

            var timeAndMaterialsService = new TimeAndMaterialsService(outage);
            var result = timeAndMaterialsService.GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules);

            Assert.Equal(4500, result[dateTimeForOvertime]);
        }
    }
}
