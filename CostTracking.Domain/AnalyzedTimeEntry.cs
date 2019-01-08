using CostTracking.Domain.Services;

namespace CostTracking.Domain
{
    public class AnalyzedTimeEntry
    {
        public bool Flagged { get; set; }
        public TimeEntryRule RuleViolated { get; set; }

        public AnalyzedTimeEntry()
        {

        }
    }
}
