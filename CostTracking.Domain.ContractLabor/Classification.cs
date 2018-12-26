namespace CostTracking.Domain.ContractLabor
{
    public abstract class Classification
    {
        public int VendorProfileId { get; private set; }
        protected virtual decimal StraightTimeRate { get; set; }
        protected virtual decimal OvertimeRate { get; set; }
        protected VendorProfile VendorProfile { get; set; }

        public Classification(int vendorProfileId)
        {
            VendorProfileId = vendorProfileId;
        }

        public virtual decimal GetRate(WorkWeekToDate workWeek)
        {
            if (NoOvertimeHours(workWeek, VendorProfile.OvertimeStartPoint))
                return StraightTimeRate;
            else if (AllOvertimeHours(workWeek, VendorProfile.OvertimeStartPoint))
                return OvertimeRate;

            return CalculateSplitRate(workWeek, VendorProfile.OvertimeStartPoint);
        }

        protected bool AllOvertimeHours(WorkWeekToDate workWeek, int overtimeStartPoint)
        {
            return overtimeStartPoint < workWeek.HoursWorkedWeekToDate;
        }

        protected bool NoOvertimeHours(WorkWeekToDate workWeek, int overtimeStartPoint)
        {
            return overtimeStartPoint > (workWeek.HoursWorkedWeekToDate + workWeek.ScheduledHoursForDate);
        }

        protected decimal CalculateSplitRate(WorkWeekToDate workWeek, int overtimeStartPoint)
        {
            var straightTimePercentage = (overtimeStartPoint - workWeek.HoursWorkedWeekToDate) / workWeek.ScheduledHoursForDate;
            return straightTimePercentage * StraightTimeRate + (1 - straightTimePercentage) * OvertimeRate;
        }
    }
}
