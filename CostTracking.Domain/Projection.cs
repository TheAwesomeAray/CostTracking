using CostTracking.Domain.Interface;
using System.Collections.Generic;

namespace CostTracking.Domain
{
    public class Projection : GLAccount
    {
        public int Id { get; private set; }
        public int RevisionNumber { get; private set; }
        public string GLAccountString { get; private set; }
        public CostClassification CostClassification { get; set; }
        public ICollection<ProjectionLineItem> LineItems { get; private set; }

        public Projection(string glAccount)
        {
            RevisionNumber = 0;
            LineItems = new List<ProjectionLineItem>();
            GLAccountString = glAccount;
        }

        public Projection()
        {
            RevisionNumber = 0;
            LineItems = new List<ProjectionLineItem>();
        }

        public Projection CreateRevision()
        {
            Id = 0;
            RevisionNumber++;

            foreach (var item in LineItems) item.Id = 0;

            return this;
        }

        public bool OriginalProjection => RevisionNumber == 0;

        public void AddProjectionLineItem(ProjectionLineItem projectionLineItem)
        {
            LineItems.Add(projectionLineItem);
        }
    }
}
