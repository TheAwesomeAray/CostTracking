namespace CostTracking.Domain.Interface
{
    public interface GLAccount
    {
        string GLAccountString { get; }
        CostClassification CostClassification { get; set; }
    }
}
