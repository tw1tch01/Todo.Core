using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Services.TodoItems.Commands.Actions.ResetItem
{
    public interface IResetItemService
    {
        Task ResetItem(Guid itemId);
    }
}
