namespace CostTracking.Domain.ContractLabor
{
    public class CompanyClassification : Classification
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        private bool Exempt { get; set; }
        protected override decimal OvertimeRate => StraightTimeRate * 1.5M;

        public CompanyClassification(string name, decimal straightTimeRate, int vendorProfileId, bool exempt) : base(vendorProfileId)
        {
            Name = name;
            StraightTimeRate = straightTimeRate;
        }

        public override decimal GetRate(WorkWeekToDate workWeekToDate)
        {
            if (Exempt)
                return StraightTimeRate;

            return base.GetRate(workWeekToDate);
        }
    }
}
