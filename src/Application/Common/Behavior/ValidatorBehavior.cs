using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Behavior
{
    public class ValidatorBehavior<TRequest, TReponse> 
        : IPipelineBehavior<TRequest, TReponse> where TRequest : notnull
    {
        private IEnumerable<IValidator<TRequest>> _validators;
        private readonly IHttpContextAccessor _context;
        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators, IHttpContextAccessor context)
        {
            _context = context;
            _validators = validators;
        }
        public async Task<TReponse> Handle(TRequest request, RequestHandlerDelegate<TReponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validatorResult = await Task.WhenAll(
                    _validators.Select(x => x.
                    ValidateAsync(context, cancellationToken)
                    ));

                var fail = validatorResult
                    .Where(x => x.Errors.Count != 0)
                    .SelectMany(x => x.Errors)
                    .ToList();
                if (fail.Count != 0)
                {
                    throw new ValidationException(fail);
                };
            }
            return await next();
        }
    }
}
