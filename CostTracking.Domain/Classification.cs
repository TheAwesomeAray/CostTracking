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

        internal decimal GetRate(decimal hoursWorked, HoursSchedule hoursSchedule)
        {
            if (hoursWorked < hoursSchedule.OvertimeStartPoint)
                return StraightTimeRate;

            return OvertimeRate;
        }
    }
}