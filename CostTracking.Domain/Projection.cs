using CostTracking.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CostTracking.Domain
{
    public class Projection : GLAccount
    {
        public int Id { get; private set; }
        public int RevisionNumber { get; private set; }
        public string GLAccountString { get; private set; }
        public CostClassification CostClassification { get; set; }
        public ICollection<ProjectionLineItem> LineItems { get; private set; }
        private bool Locked { get; set; }

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
            RevisionNumber++;
            Locked = true;

            return Clone();
        }

        private Projection Clone()
        {
            return MemberwiseClone() as Projection;
        }

        public bool OriginalProjection => RevisionNumber == 0;

        public void AddProjectionLineItem(ProjectionLineItem projectionLineItem)
        {
            LineItems.Add(projectionLineItem);
        }

        public void UpdateLineItemAmount(int lineItemId, decimal newAmount)
        {
            if (!Locked)
            LineItems.Single(x => x.Id == lineItemId).SetAmount(newAmount);
        }
    }
}
