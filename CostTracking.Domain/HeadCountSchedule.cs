using System.Collections.Generic;

namespace CostTracking.Domain
{
    public class HeadCountSchedule
    {
        public Classification Classification { get; private set; }
        private ICollection<HeadCountEntry> headCountEntries { get; set; }
        public IReadOnlyCollection<HeadCountEntry> HeadCountEntries => headCountEntries as IReadOnlyCollection<HeadCountEntry>;

        public HeadCountSchedule(ICollection<HeadCountEntry> headCountEntries, Classification classification)
        {
            this.headCountEntries = headCountEntries;
            Classification = classification;
        }
    }
}
