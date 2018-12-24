using System.Collections.Generic;

namespace CostTracking.Domain.ContractLabor
{
    public class HeadCountSchedule
    {
        public VendorClassification Classification { get; private set; }
        public IEnumerable<HeadCountEntry> HeadCountEntries { get; set; }

        public HeadCountSchedule(ICollection<HeadCountEntry> headCountEntries, VendorClassification classification)
        {
            HeadCountEntries = headCountEntries;
            Classification = classification;
        }
    }
}
