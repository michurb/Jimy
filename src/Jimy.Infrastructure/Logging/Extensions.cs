using Jimy.Application.Abstraction;
using Jimy.Infrastructure.Logging.Decorators;
using Microsoft.Extensions.DependencyInjection;

namespace Jimy.Infrastructure.Logging;

internal static class Extensions
{
    public static IServiceCollection AddCustomLogging(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

        return services;
    }
}