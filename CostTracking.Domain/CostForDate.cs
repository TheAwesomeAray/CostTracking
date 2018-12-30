using System;

namespace CostTracking.Domain
{
    public class CostForDate
    {
        public decimal Cost { get; private set; }
        public DateTime Date { get; private set; }

        public CostForDate(decimal cost, DateTime date)
        {
            Cost = cost;
            Date = date;
        }
    }
}
