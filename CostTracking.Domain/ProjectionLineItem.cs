namespace CostTracking.Domain
{
    public class ProjectionLineItem
    {
        public int Id { get; private set; }
        public decimal? Amount { get; private set; }
        public decimal OriginalAmount { get; set; }

        public ProjectionLineItem(decimal originalAmount)
        {
            OriginalAmount = originalAmount;
            Amount = originalAmount;
        }

        internal void SetAmount(decimal amount)
        {
            Amount = amount;
        }

        internal ProjectionLineItem Clone()
        {
            var clone = MemberwiseClone() as ProjectionLineItem;
            Id = 0;
            return clone;
        }
    }
}
