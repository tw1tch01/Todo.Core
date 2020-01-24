using System;

namespace Todo.Domain.Common
{
    public interface ICreatedAudit
    {
        string CreatedBy { get; }
        DateTime CreatedOn { get; }
        string CreatedProcess { get; }
    }
}