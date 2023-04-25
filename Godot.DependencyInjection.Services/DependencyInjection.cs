using Godot.DependencyInjection.Services.Input;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGodotServices(this IServiceCollection services)
        {
            services.AddGodotInputService();
            return services;
        }

        public static IServiceCollection AddGodotInputService(this IServiceCollection services)
        {
            services.AddSingleton<IInputService, InputService>();
            return services;
        }
    }
}
