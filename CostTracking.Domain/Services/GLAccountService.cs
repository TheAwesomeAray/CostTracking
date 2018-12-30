using CostTracking.Domain.Interface;

namespace CostTracking.Domain.Services
{
    public static class GLAccountService
    {
        private static int projectSegmentLocation = 3;

        public static void PopulateGLAccountFields(GLAccount glAccount)
        {
            string[] glAccountFields = glAccount.GLAccountString.Split("-");

            glAccount.CostClassification = GetCostClassification(glAccountFields[projectSegmentLocation]);
        }

        private static CostClassification GetCostClassification(string costClassification)
        {
            switch (costClassification)
            {
                case "OUT":
                    return CostClassification.Outage;
                case "NONOUT:":
                    return CostClassification.NonOutage;
                default:
                    return CostClassification.Capital;
            }
        }
    }
}
