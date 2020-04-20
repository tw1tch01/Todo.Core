using System.Collections.Generic;
using FluentValidation.Results;

namespace Todo.Services.TodoItems.Validation
{
    public class InvalidDtoResult : ItemInvalidResult
    {
        private const string _message = "One or more validation failures have occurred.";

        public InvalidDtoResult(ICollection<ValidationFailure> failures)
            : base(_message)
        {
            Data[_failuresKey] = GetValidationFailures(failures);
        }
    }
}