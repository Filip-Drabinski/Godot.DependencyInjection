using Microsoft.Extensions.DependencyInjection;

namespace Godot.DependencyInjection.Core.UnitTests.FirstAssemblyToScan;

public class ServiceRegistration: IServicesConfigurator
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<string>(x => Guid.NewGuid().ToString());
    }
}