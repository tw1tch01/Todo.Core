using System;
using System.Threading.Tasks;

namespace Todo.Services.TodoItems.Commands.Actions.CancelItem
{
    public interface ICancelItemService
    {
        Task CancelItem(Guid itemId);
    }
}