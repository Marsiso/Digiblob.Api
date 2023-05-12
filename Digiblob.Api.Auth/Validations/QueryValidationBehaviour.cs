using Digiblob.Api.Auth.Queries.Interfaces;

namespace Digiblob.Api.Auth.Validations;

/// <summary>
///     Model validation processing service.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public sealed class QueryValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IQuery<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public QueryValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Check available validators
        if (!_validators.Any())
        {
            return await next();
        }
        
        // Model validation
        var context = new ValidationContext<TRequest>(request);
        var validationFailures = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure != null)
            .GroupBy(
                validationFailure => validationFailure.PropertyName,
                validationFailure => validationFailure.ErrorMessage,
                (propertyName, validationFailures) => new
                {
                    Key = propertyName,
                    Values = validationFailures.Distinct().ToArray()
                })
            .ToDictionary(propertyName => propertyName.Key, validationFailures => validationFailures.Values);
        
        // Raise an exception in case of invalid model
        if (validationFailures.Any())
        {
            throw new UnprocessableEntityException("Object sent from the client is invalid", validationFailures);
        }
        
        return await next();
    }
}