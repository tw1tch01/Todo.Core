using System;
using Todo.DomainModels.TodoItems.Events.ResetItem;
using Todo.Services.Workflows;

namespace Todo.Services.TodoItems.Events.ResetItem
{
    public class ItemResetProcess : ItemReset, IWorkflowProcess
    {
        public ItemResetProcess(Guid itemId, DateTime resetOn) : base(itemId, resetOn)
        {
        }
    }
}