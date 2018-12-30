using CostTracking.Domain;
using CostTracking.Domain.Commands;
using System;
using Xunit;

namespace CostTracking.Tests
{
    public class ScheduleShould
    {
        private Outage outage;

        public ScheduleShould()
        {
            var command = new OutageCreateCommand()
            {
                OutageStartDate = DateTime.Parse("1/1/2018"),
                OutageEndDate = DateTime.Parse("1/30/2018")
            };
            outage = new Outage(command);
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
