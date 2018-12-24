using System;

namespace CostTracking.Domain.ContractLabor
{
    public class HeadCountEntry
    {
        public int HeadCount { get; private set; }
        public DateTime Date { get; private set; }

        public HeadCountEntry(int headCount, DateTime date)
        {
            HeadCount = headCount;
            Date = date;
        }
    }
}
