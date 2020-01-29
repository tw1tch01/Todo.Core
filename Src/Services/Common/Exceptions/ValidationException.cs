using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Todo.Services.Common.Exceptions
{
    public class ValidationException : Exception
    {
        internal const string _errorMessage = "One or more validation failures have occured.";

        public ValidationException()
            : base(_errorMessage)
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(ICollection<ValidationFailure> failures)
            : base(_errorMessage)
        {
            Failures = GetValidationFailures(failures);
        }

        public IDictionary<string, string[]> Failures { get; private set; }

        internal static Dictionary<string, string[]> GetValidationFailures(ICollection<ValidationFailure> failures)
        {
            if (!failures?.Any() ?? true) return new Dictionary<string, string[]>();

            return (from failure in failures
                    group failure by failure.PropertyName into gr
                    let errors = gr.Select(a => a.ErrorMessage).Distinct()
                    where errors != null && errors.Any()
                    select new KeyValuePair<string, IEnumerable<string>>(gr.Key, errors)).ToDictionary(k => k.Key, v => v.Value.ToArray());
        }
    }
}