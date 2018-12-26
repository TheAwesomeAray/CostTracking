using CostTracking.Domain;
using System.Linq;
using Xunit;

namespace CostTracking.Tests
{
    public class ProjectionShould
    {
        private Projection defaultProjection;

        public ProjectionShould()
        {
            defaultProjection = new Projection();
        }

        [Fact]
        public void BeClassifiedAsOriginalProjectionIfNoRevisionsHaveBeenCreated()
        {
            defaultProjection = new Projection();

            Assert.True(defaultProjection.OriginalProjection);
        }

        [Fact]
        public void CreateRevisionsWithIncrementedRevisionNumber()
        {
            defaultProjection.CreateRevision();

            Assert.True(defaultProjection.RevisionNumber == 1);
        }

        [Fact]
        public void AddProjectionLineItems()
        {
            defaultProjection.AddProjectionLineItem(new ProjectionLineItem(1));
            defaultProjection.AddProjectionLineItem(new ProjectionLineItem(1));

            Assert.True(defaultProjection.LineItems.Count == 2);
        }

        [Fact]
        public void CreateRevisionWithMatchingFields()
        {
            var projectionLineItem1 = new ProjectionLineItem(300);
            var projectionLineItem2 = new ProjectionLineItem(600);
            projectionLineItem2.SetAmount(400);
            defaultProjection.AddProjectionLineItem(projectionLineItem1);
            defaultProjection.AddProjectionLineItem(projectionLineItem2);

            var newRevision = defaultProjection.CreateRevision();

            Assert.True(newRevision.LineItems.Sum(l => l.Amount) == defaultProjection.LineItems.Sum(l => l.Amount));
            Assert.True(newRevision.LineItems.Sum(l => l.OriginalAmount) == defaultProjection.LineItems.Sum(l => l.OriginalAmount));
        }
    }
}
