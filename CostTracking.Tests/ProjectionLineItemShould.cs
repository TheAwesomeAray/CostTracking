using CostTracking.Domain;
using Xunit;

namespace CostTracking.Tests
{
    public class ProjectionLineItemShould
    {
        [Fact]
        public void Set_amount()
        {
            decimal amount = 500;
            var projection = new ProjectionLineItem(300);
            projection.SetAmount(500);

            Assert.True(projection.Amount == amount);
        }

        [Fact]
        public void Allow_overriding_of_projection_amount()
        {
            decimal amount = 500;
            var projection = new ProjectionLineItem(300);
            projection.SetAmount(amount);

            Assert.True(projection.Amount == amount);
        }

        [Fact]
        public void Not_override_original_projection()
        {
            decimal originalAmount = 500;
            var projection = new ProjectionLineItem(originalAmount);
            projection.SetAmount(300);

            Assert.True(projection.OriginalAmount == originalAmount);
        }
    }
}
