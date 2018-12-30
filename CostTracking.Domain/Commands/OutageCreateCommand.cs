using System;
using System.Collections.Generic;
using System.Text;

namespace CostTracking.Domain.Commands
{
    public class OutageCreateCommand
    {
        public DateTime OutageStartDate { get; set; }
        public DateTime OutageEndDate { get; set; }
        public DateTime PayPeriodStartDate { get; set; }
    }
}
