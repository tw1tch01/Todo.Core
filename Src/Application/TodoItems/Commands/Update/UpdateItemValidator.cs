using FluentValidation;

namespace Todo.Application.TodoItems.Commands.Update
{
    internal class UpdateItemValidator : AbstractValidator<UpdateItemRequest>
    {
        public UpdateItemValidator()
        {
            RuleFor(a => a.ItemDto).InjectValidator();
        }
    }
}