using Godot.DependencyInjection.Services;

namespace Godot.DependencyInjection.Scanning.Models.Provider;


internal interface IProviderMetadata
{
    public void Add(IServiceProvider serviceProvider, object instance);
    public void Remove(IServiceProvider serviceProvider, object instance);
}
internal struct ProviderMetadata<TProvider,TProvided> : IProviderMetadata
{
    public void Add(IServiceProvider serviceProvider, object instance) => AddTyped(serviceProvider, (TProvider)instance);
    public void Remove(IServiceProvider serviceProvider, object instance) => RemoveTyped(serviceProvider, (TProvider)instance);

    private void AddTyped(IServiceProvider serviceProvider, TProvider instance)
    {
        var registrar = (IProviderManager<TProvider, TProvided>)serviceProvider.GetService(typeof(IProviderManager<TProvider, TProvided>))!;
        registrar.Add(instance);
    }
    private void RemoveTyped(IServiceProvider serviceProvider, TProvider instance)
    {
        var registrar = (IProviderManager<TProvider, TProvided>)serviceProvider.GetService(typeof(IProviderManager<TProvider, TProvided>))!;
        registrar.Remove(instance);
    }

}