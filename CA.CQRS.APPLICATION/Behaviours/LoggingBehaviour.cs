using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CA.CQRS.APPLICATION.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Request: {RequestName} at {DateTime}",
                typeof(TRequest).Name, DateTime.UtcNow);

            var response = await next();

            _logger.LogInformation("Completed Request: {RequestName} at {DateTime}",
                typeof(TRequest).Name, DateTime.UtcNow);

            return response;
        }
    }
}
