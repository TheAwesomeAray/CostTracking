using CostTracking.Domain;
using CostTracking.Domain.ContractLabor;
using CostTracking.Domain.ContractLabor.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace CostTracking.Tests
{
    public class ProjectedLaborCostsServiceShould
    {
        private Outage outage;
        private VendorClassification vendorClassification;
        private HoursSchedule hoursSchedule;

        public ProjectedLaborCostsServiceShould()
        {
            outage = new Outage(DateTime.Parse("2/1/2018"), DateTime.Parse("2/28/2018"));
            vendorClassification = Helper.GetVendorClassification("Boilermaker", 45, 60);
            hoursSchedule = Helper.GetHoursSchedule(8, 12, 10);
        }

        [Fact]
        public void GenerateCostForStraightTimePreOrPostOutage()
        {
            var dateTimeForCost = DateTime.Parse("1/15/2018");
            var headCountEntries = new List<HeadCountEntry>() { new HeadCountEntry(10, dateTimeForCost) };
            var headCountSchedules = new List<HeadCountSchedule>() { new HeadCountSchedule(headCountEntries, vendorClassification) };
            var timeAndMaterialsService = new ProjectedLaborCostService(outage);

            var result = timeAndMaterialsService.GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules);

            Assert.Equal(4500, result[dateTimeForCost]);
        }

        [Fact]
        public void GenerateCostForStraightTimeDuringOutageWeekday()
        {
            var startDate = DateTime.Parse("2/15/2018");
            var headCountSchedules = Helper.CreateHeadCountSchedule(10, startDate, 0, vendorClassification);
            var timeAndMaterialsService = new ProjectedLaborCostService(outage);

            var result = timeAndMaterialsService.GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules);

            Assert.Equal(3600, result[startDate]);
        }

        [Fact]
        public void GenerateCostForOvertimeDuringOutageWeekday()
        {
            var startDate = DateTime.Parse("2/12/2018");
            var daysToAdd = 5;
            var timeAndMaterialsService = new ProjectedLaborCostService(outage);
            var headCountSchedules = Helper.CreateHeadCountSchedule(10, startDate, daysToAdd, vendorClassification);

            var result = timeAndMaterialsService.GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules);

            Assert.Equal(7200, result[startDate.AddDays(daysToAdd)]);
        }

        [Fact]
        public void GenerateCostForPartialStraightTimeAndPartialOvertime()
        {
            var startDate = DateTime.Parse("2/11/2018");
            var daysToAdd = 4;
            var timeAndMaterialsService = new ProjectedLaborCostService(outage);
            var headCountSchedules = Helper.CreateHeadCountSchedule(10, startDate, daysToAdd, vendorClassification);

            var result = timeAndMaterialsService.GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules);

            Assert.Equal(4200, result[startDate.AddDays(daysToAdd)]);
        }

        [Fact]
        public void UseStraightTimeRateForExemptCompanyClassifications()
        {
            var startDate = DateTime.Parse("2/11/2018");
            var daysToAdd = 4;
            var companyClassification = Helper.GetCompanyClassification("test", 45, true);
            var timeAndMaterialsService = new ProjectedLaborCostService(outage);
            var headCountSchedules = Helper.CreateHeadCountSchedule(10, startDate, daysToAdd, companyClassification);

            var result = timeAndMaterialsService.GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules);

            Assert.Equal(4500, result[startDate.AddDays(daysToAdd)]);
        }

        [Fact]
        public void UseHolidayRateForVendorProfilesWithHolidaysOnThatHoliday()
        {
            var holiday = DateTime.Parse("12/25/2018");
            var daysToAdd = 0;
            var vendorClassification = Helper.GetVendorClassificationWithHoliday(90, holiday);
            var timeAndMaterialsService = new ProjectedLaborCostService(outage);
            var headCountSchedules = Helper.CreateHeadCountSchedule(10, holiday, daysToAdd, vendorClassification);

            var result = timeAndMaterialsService.GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules);

            Assert.Equal(9000, result[holiday]);
        }
    }
}
