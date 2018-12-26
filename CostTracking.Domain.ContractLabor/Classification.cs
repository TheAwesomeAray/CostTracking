namespace CostTracking.Domain.ContractLabor
{
    public abstract class Classification
    {
        public int VendorProfileId { get; private set; }
        protected virtual decimal StraightTimeRate { get; set; }
        protected virtual decimal OvertimeRate { get; set; }
        private VendorProfile VendorProfile { get; set; }

        public Classification(int vendorProfileId)
        {
            VendorProfileId = vendorProfileId;
        }

        public virtual decimal GetRate(decimal hoursWorked, decimal scheduledHours)
        {
            if (NoOvertimeHours(hoursWorked, scheduledHours, VendorProfile.OvertimeStartPoint))
                return StraightTimeRate;
            else if (AllOvertimeHours(hoursWorked, scheduledHours, VendorProfile.OvertimeStartPoint))
                return OvertimeRate;

            return CalculateSplitRate(hoursWorked, VendorProfile.OvertimeStartPoint, scheduledHours);
        }

        protected bool AllOvertimeHours(decimal hoursWorked, decimal scheduledHours, int overtimeStartPoint)
        {
            return overtimeStartPoint < hoursWorked;
        }

        protected bool NoOvertimeHours(decimal hoursWorked, decimal scheduledHours, int overtimeStartPoint)
        {
            return overtimeStartPoint > (hoursWorked + scheduledHours);
        }

        protected decimal CalculateSplitRate(decimal hoursWorked, int overtimeStartPoint, decimal scheduledHours)
        {
            var straightTimePercentage = (overtimeStartPoint - hoursWorked) / scheduledHours;
            return straightTimePercentage * StraightTimeRate + (1 - straightTimePercentage) * OvertimeRate;
        }
    }
}
