using System;
using System.Threading.Tasks;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.TodoItems.Commands.DeleteItem
{
    public interface IDeleteItemService
    {
        Task<ItemValidationResult> DeleteItem(Guid itemId);
    }
}