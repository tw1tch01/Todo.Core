using System.Collections.Generic;
using FluentValidation.Results;

namespace Todo.Services.TodoNotes.Validation
{
    public class InvalidDtoResult : NoteInvalidResult
    {
        private const string _message = "One or more validation failures have occurred.";

        public InvalidDtoResult(ICollection<ValidationFailure> failures)
            : base(_message)
        {
            Data[_failuresKey] = GetValidationFailures(failures);
        }
    }
}