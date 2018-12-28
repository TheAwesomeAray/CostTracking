using CostTracking.Domain;
using System.Linq;
using Xunit;

namespace CostTracking.Tests
{
    public class ProjectionLineItemShould
    {
        public ProjectionLineItemShould()
        {

        }

        [Fact]
        public void SetAmount()
        {
            decimal amount = 500;
            int id = 1;
            var projection = new Projection("");
            projection.AddProjectionLineItem(Helper.CreateProjectionLineItem(amount, id));
            projection.UpdateLineItemAmount(id, amount);

            Assert.True(projection.LineItems.Single().Amount == amount);
        }

        [Fact]
        public void AllowOverridingOfProjectionAmount()
        {
            decimal amount = 500;
            int id = 1;
            var projection = new Projection("");
            projection.AddProjectionLineItem(Helper.CreateProjectionLineItem(300, id));
            projection.UpdateLineItemAmount(id, amount);

            Assert.True(projection.LineItems.Single().Amount == amount);
        }

        [Fact]
        public void NotOverrideOriginalProjection()
        {
            decimal originalAmount = 500;
            int id = 1;
            var projection = new Projection("");
            projection.AddProjectionLineItem(Helper.CreateProjectionLineItem(originalAmount, id));
            projection.UpdateLineItemAmount(id, 500);

            Assert.True(projection.LineItems.Single().OriginalAmount == originalAmount);
        }
    }
}
