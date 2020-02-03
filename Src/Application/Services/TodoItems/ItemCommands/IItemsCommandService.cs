using Todo.Services.TodoItems.Commands.CancelItem;
using Todo.Services.TodoItems.Commands.CompleteItem;
using Todo.Services.TodoItems.Commands.CreateItem;
using Todo.Services.TodoItems.Commands.DeleteItem;
using Todo.Services.TodoItems.Commands.ResetItem;
using Todo.Services.TodoItems.Commands.StartItem;
using Todo.Services.TodoItems.Commands.UpdateItem;

namespace Todo.Application.Services.TodoItems.ItemCommands
{
    public interface IItemsCommandService : ICreateItemService,
                                            IDeleteItemService,
                                            IUpdateItemService,
                                            ICancelItemService,
                                            ICompleteItemService,
                                            IResetItemService,
                                            IStartItemService
    {
    }
}