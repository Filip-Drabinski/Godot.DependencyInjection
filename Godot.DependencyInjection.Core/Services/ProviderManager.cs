namespace Godot.DependencyInjection.Services;
internal interface IProviderManager<in TProvider, TProvided>
{
    IEnumerable<TProvided> Provide();
    void Add(TProvider instance);
    void Remove(TProvider instance);
}

internal class ProviderManager<TProvider, TProvided> where TProvider : INodeProvider<TProvided>
{
    private HashSet<TProvider> _providers;

    public ProviderManager()
    {
        _providers = new HashSet<TProvider>();
    }
    IEnumerable<TProvided> Provide()
    {
        var result = _providers.Select(x =>
        {
            x.Provide(out var provided);
            return provided;
        });
        return result;
    }

    void Add(TProvider instance)
    {
        _providers.Add(instance);
    }

    void Remove(TProvider instance)
    {
        _providers.Remove(instance);
    }
}