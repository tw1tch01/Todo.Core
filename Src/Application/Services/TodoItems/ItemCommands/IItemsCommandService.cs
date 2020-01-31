using Todo.Services.TodoItems.Commands.Actions.CancelItem;
using Todo.Services.TodoItems.Commands.Actions.CompleteItem;
using Todo.Services.TodoItems.Commands.Actions.ResetItem;
using Todo.Services.TodoItems.Commands.Actions.StartItem;
using Todo.Services.TodoItems.Commands.CreateItem;
using Todo.Services.TodoItems.Commands.DeleteItem;
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