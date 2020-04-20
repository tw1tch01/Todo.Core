using System;

namespace Todo.Services.TodoItems.Validation
{
    public class ItemResetResult : ItemValidResult
    {
        private const string _message = "Item successfully reset.";
        private const string _resetOnKey = "ResetOn";

        public ItemResetResult(Guid itemId, DateTime resetOn)
            : base(itemId, _message)
        {
            Data[_resetOnKey] = resetOn;
        }
    }
}