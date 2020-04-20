using FluentValidation;

namespace Todo.DomainModels.TodoItems.Validators
{
    public static class ItemValidatorFactory
    {
        public static AbstractValidator<CreateItemDto> CreateItemValidator()
        {
            return new CreateItemValidator();
        }

        public static AbstractValidator<UpdateItemDto> UpdateItemValidator()
        {
            return new UpdateItemValidator();
        }
    }
}