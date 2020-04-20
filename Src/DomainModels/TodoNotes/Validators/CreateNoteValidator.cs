using FluentValidation;

namespace Todo.DomainModels.TodoNotes.Validators
{
    public class CreateNoteValidator : AbstractValidator<CreateNoteDto>
    {
        public CreateNoteValidator()
        {
            RuleFor(a => a.Comment).NotEmpty();
            RuleFor(a => a.ItemId).NotEmpty();
        }
    }
}