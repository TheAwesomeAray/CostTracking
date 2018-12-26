using CostTracking.Domain;
using CostTracking.Domain.ContractLabor;
using CostTracking.Domain.ContractLabor.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace CostTracking.Tests
{
    public class ScheduleShould
    {
        private Outage outage;

        public ScheduleShould()
        {
            outage = new Outage(DateTime.Parse("1/1/2018"), DateTime.Parse("1/30/2018"));
        }

        [Fact]
        public void DetermineScheduledHoursForWeekday()
        {
            int outageWeekDaySchedule = 8;
            var hoursSchedule = Helper.GetHoursSchedule(outageWeekDaySchedule, 0, 0);
            var weekDayDate = DateTime.Parse("1/4/2018");

            Assert.Equal(outageWeekDaySchedule, hoursSchedule.GetScheduledHoursForDate(outage, weekDayDate));
        }

        [Fact]
        public void DetermineScheduledHoursForWeekend()
        {
            var weekEndDate = DateTime.Parse("1/6/2018");
            int outageWeekendSchedule = 8;
            var hoursSchedule = Helper.GetHoursSchedule(0, outageWeekendSchedule, 0);

            Assert.Equal(outageWeekendSchedule, hoursSchedule.GetScheduledHoursForDate(outage, weekEndDate));
        }

        [Fact]
        public void DetermineScheduledHoursForPreAndPostOutage()
        {
            var postOutageDate = DateTime.Parse("2/1/2018");
            int prePostOutageSchedule = 8;
            var hoursSchedule = Helper.GetHoursSchedule(0, 0, prePostOutageSchedule);

            Assert.Equal(prePostOutageSchedule, hoursSchedule.GetScheduledHoursForDate(outage, postOutageDate));
        }
    }
}
