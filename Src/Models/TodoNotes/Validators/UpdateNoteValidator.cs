using FluentValidation;

namespace Todo.Models.TodoNotes.Validators
{
    public class UpdateNoteValidator : AbstractValidator<UpdateNoteDto>
    {
        public UpdateNoteValidator()
        {
            RuleFor(a => a.Comment).NotEmpty();
        }
    }
}