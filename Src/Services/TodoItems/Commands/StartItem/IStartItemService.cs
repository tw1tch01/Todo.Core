using System;
using System.Threading.Tasks;

namespace Todo.Services.TodoItems.Commands.StartItem
{
    public interface IStartItemService
    {
        Task StartItem(Guid itemId);
    }
}