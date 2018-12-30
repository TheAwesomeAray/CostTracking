using CostTracking.Domain.Commands;
using System;

namespace CostTracking.Domain
{
    public class Outage
    {
        public DateTime OutageStartDate { get; private set; }
        public DateTime OutageEndDate { get; private set; }
        public DateTime PayPeriodStartDate { get; private set; }

        public Outage(OutageCreateCommand command)
        {
            OutageStartDate = command.OutageStartDate;
            OutageEndDate = command.OutageEndDate;
            PayPeriodStartDate = command.PayPeriodStartDate;
        }

        public bool DuringOutage(DateTime date)
        {
            return date > OutageStartDate && date < OutageEndDate;
        }
    }
}
