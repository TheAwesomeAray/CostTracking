namespace CostTracking.Domain
{
    public class ProjectionLineItem
    {
        public int Id { get; internal set; }
        public decimal? Amount { get; private set; }
        public decimal OriginalAmount { get; set; }

        public ProjectionLineItem(decimal originalAmount)
        {
            OriginalAmount = originalAmount;
        }

        public void SetAmount(decimal amount)
        {
            Amount = amount;
        }
    }
}
