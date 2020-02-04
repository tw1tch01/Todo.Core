using FluentValidation;

namespace Todo.DomainModels.TodoItems.Validators
{
    public class CreateItemValidator : AbstractValidator<CreateItemDto>
    {
        public CreateItemValidator()
        {
            RuleFor(a => a.Name).NotEmpty().MaximumLength(64);
            RuleFor(a => a.Description).NotEmpty();
            RuleFor(a => a.Rank).GreaterThanOrEqualTo(0);
        }
    }
}