using System;
using System.Threading.Tasks;

namespace Todo.Services.TodoItems.Commands.Actions.StartItem
{
    public interface IStartItemService
    {
        Task StartItem(Guid itemId);
    }
}