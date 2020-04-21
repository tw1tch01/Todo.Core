using System;

namespace Todo.Domain.Common
{
    public abstract class BaseEntity : ICreatedAudit, IModifiedAudit
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedProcess { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedProcess { get; set; }
    }
}