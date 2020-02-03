using System;
using Todo.DomainModels.TodoItems.Events.ResetItem;
using Todo.Services.External.Workflows;

namespace Todo.Services.External.Events.TodoItems.ResetItem
{
    public class ItemResetProcess : ItemReset, IWorkflowProcess
    {
        public ItemResetProcess(Guid itemId, DateTime resetOn) : base(itemId, resetOn)
        {
        }
    }
}