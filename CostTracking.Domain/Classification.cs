using System;

namespace CostTracking.Domain
{
    public class Classification
    {
        public string Name { get; private set; }
        public decimal StraightTimeRate { get; private set; }
        public decimal OvertimeRate { get; private set; }

        public Classification(string name, decimal straightTimeRate, decimal overtimeRate)
        {
            Name = name;
            StraightTimeRate = straightTimeRate;
            OvertimeRate = overtimeRate;
        }

        internal decimal GetRate(decimal hoursWorked, HoursSchedule hoursSchedule, decimal scheduledHours)
        {
            if (NoOvertimeHours(hoursWorked, scheduledHours, hoursSchedule.OvertimeStartPoint))
                return StraightTimeRate;
            else if (AllOvertimeHours(hoursWorked, scheduledHours, hoursSchedule.OvertimeStartPoint))
                return OvertimeRate;

            return CalculateSplitRate(hoursWorked, hoursSchedule.OvertimeStartPoint, scheduledHours);
        }

        private bool AllOvertimeHours(decimal hoursWorked, decimal scheduledHours, int overtimeStartPoint)
        {
            return overtimeStartPoint < hoursWorked;
        }

        private bool NoOvertimeHours(decimal hoursWorked, decimal scheduledHours, int overtimeStartPoint)
        {
            return overtimeStartPoint > (hoursWorked + scheduledHours);
        }

        private decimal CalculateSplitRate(decimal hoursWorked, int overtimeStartPoint, decimal scheduledHours)
        {
            var straightTimePercentage = (overtimeStartPoint - hoursWorked) / scheduledHours;
            return straightTimePercentage * StraightTimeRate + (1 - straightTimePercentage) * OvertimeRate;
        }
    }
}