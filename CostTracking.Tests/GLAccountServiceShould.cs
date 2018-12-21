using CostTracking.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CostTracking.Tests
{
    public class GLAccountServiceShould
    {
        [Fact]
        public void Correctly_assign_cost_category()
        {
            var projection = new Projection("000000-000000-000000-OUT");

            GLAccountService.PopulateGLAccountFields(projection);

            Assert.True(projection.CostClassification == CostClassification.Outage);

        }
    }
}
