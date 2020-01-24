using System;

namespace Todo.Domain.Common
{
    public interface IModifiedAudit
    {
        string ModifiedBy { get; }
        DateTime? ModifiedOn { get; }
        string ModifiedProcess { get; }
    }
}