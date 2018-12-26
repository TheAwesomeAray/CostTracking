namespace CostTracking.Domain.ContractLabor
{
    public class VendorClassification : Classification
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public VendorClassification(string name, decimal straightTimeRate, decimal overtimeRate, int vendorProfileId) : base(vendorProfileId)
        {
            Name = name;
            StraightTimeRate = straightTimeRate;
            OvertimeRate = overtimeRate;
        }
    }
}