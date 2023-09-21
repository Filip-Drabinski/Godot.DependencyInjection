using Godot.DependencyInjection.Services.Input;
using Godot.DependencyInjection.Services.Logger;

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
        public static IServiceCollection AddGodotLogger(this IServiceCollection services)
        {
            services.AddLogging(x => x.AddGodot());
            return services;
        }
        
    }
}
