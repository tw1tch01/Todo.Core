using FluentValidation;

namespace Todo.Application.TodoItems.Commands.Create
{
    internal class AddChildItemValidator : AbstractValidator<AddChildItemRequest>
    {
        public AddChildItemValidator()
        {
            RuleFor(a => a.ChildItemDto).InjectValidator();
        }
    }
}