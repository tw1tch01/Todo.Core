using System;
using System.Threading.Tasks;

namespace Todo.Services.TodoItems.Commands.CancelItem
{
    public interface ICancelItemService
    {
        Task CancelItem(Guid itemId);
    }
}