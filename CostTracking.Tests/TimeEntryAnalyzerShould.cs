using CostTracking.Domain;
using CostTracking.Domain.Commands;
using CostTracking.Domain.ContractLabor;
using CostTracking.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CostTracking.Tests
{
    public class TimeEntryAnalyzerShould
    {
        private Outage outage;

        public TimeEntryAnalyzerShould()
        {
            var command = new OutageCreateCommand()
            {
                OutageStartDate = DateTime.Parse("1/1/2018"),
                OutageEndDate = DateTime.Parse("1/30/2018")
            };
            outage = new Outage(command);
        }

        [Fact]
        public void IdentifyIndividualsWithExcessiveWorkHours()
        {
            var classification = Helper.GetVendorClassification("test", 0, 0);
            var timeEntries = new List<VendorTimeEntry>()
            {
                new VendorTimeEntry(DateTime.Parse("1/2/2018"), "Andrew", "Ray", 10, classification),
                new VendorTimeEntry(DateTime.Parse("1/2/2018"), "Andrew", "Ray", 5, classification)
            };
            var costGrouping = AggregationFactory.GetIndividualAggregator(outage, null).Aggregate(timeEntries);

            var analyzedGroupings = new TimeEntryAnalyzer().Analyze(costGrouping);

            Assert.True(analyzedGroupings.First().Flagged);
            Assert.Equal(TimeEntryRule.ExcessiveHoursWorked, analyzedGroupings.First().RuleViolated);
        }
    }
}
