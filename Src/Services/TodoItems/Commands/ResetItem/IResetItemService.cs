using System;
using System.Threading.Tasks;

namespace Todo.Services.TodoItems.Commands.ResetItem
{
    public interface IResetItemService
    {
        Task ResetItem(Guid itemId);
    }
}