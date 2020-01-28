using FluentValidation;

namespace Todo.Models.TodoNotes.Validators
{
    public class CreateNoteValidator : AbstractValidator<CreateNoteDto>
    {
        public CreateNoteValidator()
        {
            RuleFor(a => a.Comment).NotEmpty();
        }
    }
}