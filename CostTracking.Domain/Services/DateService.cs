using System;

namespace CostTracking.Domain.Services
{
    public static class DateService
    {
        public static bool IsWeekDay(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday
                && date.DayOfWeek != DayOfWeek.Sunday;
        }

        public static bool IsWeekEnd(DateTime date)
        {
            return !IsWeekDay(date);
        }
    }
}
