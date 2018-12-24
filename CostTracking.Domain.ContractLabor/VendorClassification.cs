namespace CostTracking.Domain.ContractLabor
{
    public class VendorClassification
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public decimal StraightTimeRate { get; private set; }
        public decimal OvertimeRate { get; private set; }
        private VendorProfile VendorProfile { get; set; }

        public VendorClassification(string name, decimal straightTimeRate, decimal overtimeRate, int companyProfileId)
        {
            Name = name;
            StraightTimeRate = straightTimeRate;
            OvertimeRate = overtimeRate;
        }

        internal decimal GetRate(decimal hoursWorked, decimal scheduledHours)
        {
            if (NoOvertimeHours(hoursWorked, scheduledHours, VendorProfile.OvertimeStartPoint))
                return StraightTimeRate;
            else if (AllOvertimeHours(hoursWorked, scheduledHours, VendorProfile.OvertimeStartPoint))
                return OvertimeRate;

            return CalculateSplitRate(hoursWorked, VendorProfile.OvertimeStartPoint, scheduledHours);
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