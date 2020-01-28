using FluentValidation;
using Todo.DomainModels.TodoItems;

namespace Todo.DomainModels.TodoItems.Validators
{
    public class UpdateItemValidator : AbstractValidator<UpdateItemDto>
    {
        public UpdateItemValidator()
        {
            RuleFor(d => d.Name).MaximumLength(64);
            RuleFor(d => d.Description).MaximumLength(1024);
            RuleFor(d => d.Rank).GreaterThanOrEqualTo(0).When(d => d.Rank.HasValue);
        }
    }
}