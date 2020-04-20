using System;
using System.Threading.Tasks;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.TodoItems.Commands.ResetItem
{
    public interface IResetItemService
    {
        Task<ItemValidationResult> ResetItem(Guid itemId);
    }
}