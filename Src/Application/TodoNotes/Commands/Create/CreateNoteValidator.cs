using FluentValidation;

namespace Todo.Application.TodoNotes.Commands.Create
{
    internal class CreateNoteValidator : AbstractValidator<CreateNoteRequest>
    {
        public CreateNoteValidator()
        {
            RuleFor(a => a.NoteDto).InjectValidator();
        }
    }
}