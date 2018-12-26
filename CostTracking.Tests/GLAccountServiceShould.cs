using CostTracking.Domain;
using Xunit;

namespace CostTracking.Tests
{
    public class GLAccountServiceShould
    {
        [Fact]
        public void CorrectlyAssignCostCategory()
        {
            var projection = new Projection("000000-000000-000000-OUT");

            GLAccountService.PopulateGLAccountFields(projection);

            Assert.True(projection.CostClassification == CostClassification.Outage);
        }
    }
}
