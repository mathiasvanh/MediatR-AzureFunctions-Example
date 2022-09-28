using System.Text;
using FluentValidation;
using MediatR;

namespace MediatRExample.Common.Validation
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IList<IValidator<TRequest>> _validators;

        public ValidationBehavior(IList<IValidator<TRequest>> validators)
        {
            this._validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var errors = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (!errors.Any())
                return next();

            var errorBuilder = new StringBuilder();
            errorBuilder.AppendLine("Invalid command or query, reason(s): ");

            foreach (var error in errors)
            {
                errorBuilder.AppendLine("- " + error.ErrorMessage);
            }

            throw new ValidationException(errorBuilder.ToString());
        }
    }
}