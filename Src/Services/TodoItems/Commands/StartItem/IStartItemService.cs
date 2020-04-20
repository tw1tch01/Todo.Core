using System;
using System.Threading.Tasks;
using Todo.Services.TodoItems.Validation;

namespace Todo.Services.TodoItems.Commands.StartItem
{
    public interface IStartItemService
    {
        Task<ItemValidationResult> StartItem(Guid itemId);
    }
}