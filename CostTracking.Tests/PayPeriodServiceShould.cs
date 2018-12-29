using CostTracking.Domain;
using CostTracking.Domain.ContractLabor;
using CostTracking.Domain.ContractLabor.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CostTracking.Tests
{
    public class PayPeriodServiceShould
    {
        private Outage outage;
        private CompanyClassification companyClassification;
        private HoursSchedule hoursSchedule;

        public PayPeriodServiceShould()
        {
            outage = new Outage(DateTime.Parse("2/1/2018"), DateTime.Parse("2/28/2018"));
            companyClassification = Helper.GetCompanyClassification("Boilermaker", 45, false);
            hoursSchedule = Helper.GetHoursSchedule(8, 12, 10);
        }

        [Fact]
        public void GroupCostsByPayPeriod()
        {
            var startDate = DateTime.Parse("2/11/2018");
            var daysToAdd = 4;
            var companyClassification = Helper.GetCompanyClassification("test", 45, true);
            var headCountSchedules = Helper.CreateHeadCountSchedule(10, startDate, daysToAdd, companyClassification);
            var projectedCosts = new TimeAndMaterialsService(outage).GetProjectedCostsForDateRange(hoursSchedule, headCountSchedules);
            var payPeriodStartDate = DateTime.Parse("2/13/2018");

            var result = new PayPeriodService(payPeriodStartDate).GroupCostsByPayPeriod(projectedCosts);

            Assert.Equal(9000, result[payPeriodStartDate]);
            Assert.Equal(11700, result[payPeriodStartDate.AddDays(14)]);
        }
    }
}
