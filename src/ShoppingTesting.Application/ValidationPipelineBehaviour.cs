using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ShoppingTesting.Application
{
    /// <summary>
    /// Sistemden geçen query ve command'leri IValidatableObject ile valide edip, duruma göre 400 badRequest dönmek için MediatR pipeline element'i.
    /// Exception'ların httpStatusCode'larına dönmesi ConfigureProblemDetails class'ında.
    /// </summary>
    public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<ValidationPipelineBehaviour<TRequest, TResponse>> _logger;

        public ValidationPipelineBehaviour(ILogger<ValidationPipelineBehaviour<TRequest, TResponse>> logger) =>
            _logger = logger;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (request is IValidatableObject validatableObject)
            {
                var validationResults = validatableObject.Validate(new ValidationContext(request)).ToArray();
                if (validationResults.Any())
                {
                    var validationException = new RequestValidationException(validationResults);
                    _logger.LogError(validationException, $"Request Not Valid! ({request.GetType().Name})");
                    throw validationException;
                }
            }

            var response = await next();
            return response;
        }
    }
}