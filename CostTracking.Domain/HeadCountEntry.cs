using System;
using System.Collections.Generic;
using System.Text;

namespace CostTracking.Domain
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
