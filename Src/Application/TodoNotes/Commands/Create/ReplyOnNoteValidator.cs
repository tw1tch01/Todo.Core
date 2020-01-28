using FluentValidation;

namespace Todo.Application.TodoNotes.Commands.Create
{
    internal class ReplyOnNoteValidator : AbstractValidator<ReplyOnNoteRequest>
    {
        public ReplyOnNoteValidator()
        {
            RuleFor(a => a.NoteDto).InjectValidator();
        }
    }
}