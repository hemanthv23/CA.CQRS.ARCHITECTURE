using CA.CQRS.APPLICATION.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CA.CQRS.APPLICATION.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(LoggingBehaviour<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
