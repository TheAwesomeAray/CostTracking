using CostTracking.Domain;
using CostTracking.Domain.Commands;
using CostTracking.Domain.ContractLabor;
using CostTracking.Domain.ContractLabor.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace CostTracking.Tests
{
    public class HeadCountServiceShould
    {
        private HoursSchedule hoursSchedule;
        private Outage outage;

        public HeadCountServiceShould()
        {
            hoursSchedule = Helper.GetHoursSchedule(8, 12, 10);
            var command = new OutageCreateCommand()
            {
                OutageStartDate = DateTime.Parse("1/1/2018"),
                OutageEndDate = DateTime.Parse("1/30/2018")
            };
            outage = new Outage(command);
        }

        [Fact]
        public void CalculateEquivalentHeadcountsDuringOutage()
        {
            var weekDayDate = DateTime.Parse("1/4/2018");
            var daysWorked = new List<DateTime>()  { weekDayDate };
            int desiredHeadCount = 11;
            var timeEntries = Helper.GetTimeEntriesForDateRange(hoursSchedule, desiredHeadCount, daysWorked, outage, Helper.GetVendorClassification("Boilermaker", 45, 60));
            var headCountService = new HeadCountService(hoursSchedule);

            var equivalentHeadCounts = headCountService.GetEquivalentHeadCount(timeEntries, outage, hoursSchedule);

            Assert.Equal(desiredHeadCount, equivalentHeadCounts[weekDayDate]);
        }
    }
}
