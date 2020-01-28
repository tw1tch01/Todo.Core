using FluentValidation;

namespace Todo.Application.TodoNotes.Commands.Update
{
    internal class UpdateNoteValidator : AbstractValidator<UpdateNoteRequest>
    {
        public UpdateNoteValidator()
        {
            RuleFor(a => a.NoteDto).InjectValidator();
        }
    }
}