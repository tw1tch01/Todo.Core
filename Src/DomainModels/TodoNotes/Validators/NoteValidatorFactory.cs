using FluentValidation;

namespace Todo.DomainModels.TodoNotes.Validators
{
    public static class NoteValidatorFactory
    {
        public static AbstractValidator<CreateNoteDto> CreateNoteValidator()
        {
            return new CreateNoteValidator();
        }

        public static AbstractValidator<UpdateNoteDto> UpdateNoteValidator()
        {
            return new UpdateNoteValidator();
        }
    }
}