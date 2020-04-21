using System;
using System.Threading.Tasks;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.TodoItems.Commands.CancelItem
{
    public interface ICancelItemService
    {
        Task<ItemValidationResult> CancelItem(Guid itemId);
    }
}