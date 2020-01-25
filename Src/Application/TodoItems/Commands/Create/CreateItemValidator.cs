using FluentValidation;

namespace Todo.Application.TodoItems.Commands.Create
{
    internal class CreateItemValidator : AbstractValidator<CreateItemRequest>
    {
        public CreateItemValidator()
        {
            RuleFor(a => a.ItemDto).InjectValidator();
        }
    }
}