using CostTracking.Domain.Services;
using System;

namespace CostTracking.Domain.ContractLabor
{
    public class HoursSchedule
    {
        private decimal WeekDayHours { get; set; }
        private decimal WeekEndHours { get; set; }
        private decimal PrePostOutageHours { get; set; }
        private int VendorProfileId { get; set; }
        private VendorProfile VendorProfile { get; set; }

        public HoursSchedule(int weekdayHours, int weekendHours, int prePostOutageHours, int vendorProfileId)
        {
            WeekDayHours = weekdayHours;
            WeekEndHours = weekendHours;
            PrePostOutageHours = prePostOutageHours;
        }

        public decimal GetScheduledHoursForDate(Outage outage, DateTime dateTime)
        {
            if (!outage.DuringOutage(dateTime)) return PrePostOutageHours;
            else if (DateService.IsWeekDay(dateTime)) return WeekDayHours;

            return WeekEndHours;
        }

        public bool IsNewWorkWeek(HeadCountEntry entry)
        {
            return entry.Date.DayOfWeek == VendorProfile.WorkWeekStart;
        }
    }
}
