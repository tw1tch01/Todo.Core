using FluentValidation;

namespace Todo.DomainModels.TodoNotes.Validators
{
    public class UpdateNoteValidator : AbstractValidator<UpdateNoteDto>
    {
        public UpdateNoteValidator()
        {
            RuleFor(a => a.Comment).NotEmpty();
        }
    }
}