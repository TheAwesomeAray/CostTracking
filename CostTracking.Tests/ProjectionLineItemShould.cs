using CostTracking.Domain;
using Xunit;

namespace CostTracking.Tests
{
    public class ProjectionLineItemShould
    {
        [Fact]
        public void SetAmount()
        {
            decimal amount = 500;
            var projection = new ProjectionLineItem(300);
            projection.SetAmount(500);

            Assert.True(projection.Amount == amount);
        }

        [Fact]
        public void AllowOverridingOfProjectionAmount()
        {
            decimal amount = 500;
            var projection = new ProjectionLineItem(300);
            projection.SetAmount(amount);

            Assert.True(projection.Amount == amount);
        }

        [Fact]
        public void NotOverrideOriginalProjection()
        {
            decimal originalAmount = 500;
            var projection = new ProjectionLineItem(originalAmount);
            projection.SetAmount(300);

            Assert.True(projection.OriginalAmount == originalAmount);
        }
    }
}
