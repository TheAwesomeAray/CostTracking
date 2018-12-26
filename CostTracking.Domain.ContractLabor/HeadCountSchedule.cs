using System.Collections.Generic;

namespace CostTracking.Domain.ContractLabor
{
    public class HeadCountSchedule
    {
        public Classification Classification { get; private set; }
        public IEnumerable<HeadCountEntry> HeadCountEntries { get; set; }

        public HeadCountSchedule(ICollection<HeadCountEntry> headCountEntries, Classification classification)
        {
            HeadCountEntries = headCountEntries;
            Classification = classification;
        }
    }
}
