using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ValidationException = Todo.Services.Common.Exceptions.ValidationException;

namespace Todo.Services.Common.Behaviours
{
    public class RequestValidation<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICollection<AbstractValidator<TRequest>> _validators;

        public RequestValidation(ICollection<AbstractValidator<TRequest>> validators)
        {
            _validators = validators ?? new List<AbstractValidator<TRequest>>();
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationFailures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(v => v.Errors)
                .Where(e => e != null)
                .ToList();

            if (validationFailures.Any())
            {
                throw new ValidationException(validationFailures);
            }

            return next();
        }
    }
}