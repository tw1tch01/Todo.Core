using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Todo.Services.Common.Validation
{
    public abstract class ValidationResult
    {
        protected const string _failuresKey = "Failures";

        protected ValidationResult(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
            Data = new Dictionary<string, object>();
        }

        public bool IsValid { get; }
        public string Message { get; }
        public IDictionary<string, object> Data { get; }

        internal static Dictionary<string, List<string>> GetValidationFailures(ICollection<ValidationFailure> failures)
        {
            if (!failures?.Any() ?? true) return new Dictionary<string, List<string>>();

            return (from failure in failures
                    group failure by failure.PropertyName into gr
                    let errors = gr.Select(a => a.ErrorMessage).Distinct()
                    where errors != null && errors.Any()
                    select new KeyValuePair<string, IEnumerable<string>>(gr.Key, errors)).ToDictionary(k => k.Key, v => v.Value.ToList());
        }
    }
}