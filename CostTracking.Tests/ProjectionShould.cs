using CostTracking.Domain;
using System;
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
        public void Be_classified_as_original_projection_if_no_revisions_have_been_created()
        {
            defaultProjection = new Projection();

            Assert.True(defaultProjection.OriginalProjection);
        }

        [Fact]
        public void Create_revisions_with_incremented_revision_number()
        {
            defaultProjection.CreateRevision();

            Assert.True(defaultProjection.RevisionNumber == 1);
        }

        [Fact]
        public void Add_projection_line_items()
        {
            defaultProjection.AddProjectionLineItem(new ProjectionLineItem(1));
            defaultProjection.AddProjectionLineItem(new ProjectionLineItem(1));

            Assert.True(defaultProjection.LineItems.Count == 2);
        }

        [Fact]
        public void Create_revision_with_matching_fields()
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
