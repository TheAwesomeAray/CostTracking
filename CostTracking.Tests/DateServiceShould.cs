using CostTracking.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CostTracking.Tests
{
    public class DateServiceShould
    {
        [Fact]
        public void Correctly_identify_weekdays()
        {
            var dateTime = DateTime.Now;
            while (dateTime.DayOfWeek != DayOfWeek.Monday) dateTime = dateTime.AddDays(1);

            Assert.True(DateService.IsWeekDay(dateTime));

            while (dateTime.DayOfWeek != DayOfWeek.Saturday) dateTime = dateTime.AddDays(1);

            Assert.False(DateService.IsWeekDay(dateTime));
        }

        [Fact]
        public void Correctly_identify_weekends()
        {
            var dateTime = DateTime.Now;
            while (dateTime.DayOfWeek != DayOfWeek.Saturday) dateTime = dateTime.AddDays(1);

            Assert.True(DateService.IsWeekEnd(dateTime));

            while (dateTime.DayOfWeek != DayOfWeek.Monday) dateTime = dateTime.AddDays(1);

            Assert.False(DateService.IsWeekEnd(dateTime));
        }
    }
}
