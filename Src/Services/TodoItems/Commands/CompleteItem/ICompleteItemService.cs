using System;
using System.Threading.Tasks;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.TodoItems.Commands.CompleteItem
{
    public interface ICompleteItemService
    {
        Task<ItemValidationResult> CompleteItem(Guid itemId);
    }
}