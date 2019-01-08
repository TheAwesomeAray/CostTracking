using System;
using System.Collections.Generic;
using System.Linq;

namespace CostTracking.Domain.Services
{
    public class TimeEntryAnalyzer
    {
        private int excessiveHoursLimit { get; set; }

        public IEnumerable<AnalyzedTimeEntry> Analyze(Dictionary<string, decimal> costGrouping)
        {
            return costGrouping.Select(x => new AnalyzedTimeEntry()
            {
                Flagged = x.Value > excessiveHoursLimit,
                RuleViolated = TimeEntryRule.ExcessiveHoursWorked
            });
        }
    }

    public enum TimeEntryRule
    {
        ExcessiveHoursWorked
    }
}
