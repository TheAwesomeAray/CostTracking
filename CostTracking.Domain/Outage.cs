using System;

namespace CostTracking.Domain
{
    public class Outage
    {
        public DateTime OutageStartDate { get; private set; }
        public DateTime OutageEndDate { get; set; }

        public Outage(DateTime outageStartDate, DateTime outageEndDate)
        {
            OutageStartDate = outageStartDate;
            OutageEndDate = outageEndDate;
        }

        public bool DuringOutage(DateTime date)
        {
            return date > OutageStartDate && date < OutageEndDate;
        }
    }
}
