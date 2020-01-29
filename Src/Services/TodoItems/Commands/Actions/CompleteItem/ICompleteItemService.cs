using System;
using System.Threading.Tasks;

namespace Todo.Services.TodoItems.Commands.Actions.CompleteItem
{
    public interface ICompleteItemService
    {
        Task CompleteItem(Guid itemId);
    }
}