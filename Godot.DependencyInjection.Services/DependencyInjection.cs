using Godot.DependencyInjection.Services.Input;
using Godot.DependencyInjection.Services.Logger;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGodotServices(this IServiceCollection services)
        {
            services.AddGodotLogger();
            services.AddGodotInputService();
            return services;
        }

        public static IServiceCollection AddGodotInputService(this IServiceCollection services)
        {
            services.AddSingleton<IInputService, InputService>();
            return services;
        }
        public static IServiceCollection AddGodotLogger(this IServiceCollection services, Action<ILoggingBuilder>? configure = null)
        {
            services.AddLogging(x =>
            {
                x.AddGodot();
                configure?.Invoke(x);
            });
            return services;
        }
        
    }
}
