using System;
using System.Threading.Tasks;

namespace Todo.Services.TodoItems.Commands.DeleteItem
{
    public interface IDeleteItemService
    {
        Task DeleteItem(Guid itemId);
    }
}