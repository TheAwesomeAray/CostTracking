using CostTracking.Domain.Services;
using System;
using Xunit;

namespace CostTracking.Tests
{
    public class DateServiceShould
    {
        [Fact]
        public void IdentifyWeekdays()
        {
            var dateTime = DateTime.Now;
            while (dateTime.DayOfWeek != DayOfWeek.Monday) dateTime = dateTime.AddDays(1);

            Assert.True(DateService.IsWeekDay(dateTime));

            while (dateTime.DayOfWeek != DayOfWeek.Saturday) dateTime = dateTime.AddDays(1);

            Assert.False(DateService.IsWeekDay(dateTime));
        }

        [Fact]
        public void IdentifyWeekends()
        {
            var dateTime = DateTime.Now;
            while (dateTime.DayOfWeek != DayOfWeek.Saturday) dateTime = dateTime.AddDays(1);

            Assert.True(DateService.IsWeekEnd(dateTime));

            while (dateTime.DayOfWeek != DayOfWeek.Monday) dateTime = dateTime.AddDays(1);

            Assert.False(DateService.IsWeekEnd(dateTime));
        }
    }
}
