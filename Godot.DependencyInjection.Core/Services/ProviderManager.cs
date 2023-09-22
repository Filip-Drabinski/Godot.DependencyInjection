namespace Godot.DependencyInjection.Services;

internal interface IProviderManager
{
    IEnumerable<object> ProvideAsObjects();
}
internal interface IProviderManager<in TProvider, out TProvided>: IProviderManager
{
    IEnumerable<TProvided> Provide();
    void Add(TProvider instance);
    void Remove(TProvider instance);
}

internal class ProviderManager<TProvider, TProvided>:IProviderManager<TProvider,TProvided> where TProvider : INodeProvider<TProvided>
{
    private HashSet<TProvider> _providers;

    public ProviderManager()
    {
        _providers = new HashSet<TProvider>();
    }
    public IEnumerable<object> ProvideAsObjects() => Provide().Cast<object>();
    public IEnumerable<TProvided> Provide()
    {
        var result = _providers.Select(x =>
        {
            x.Provide(out var provided);
            return provided;
        });
        return result;
    }

    public void Add(TProvider instance)
    {
        _providers.Add(instance);
    }

    public void Remove(TProvider instance)
    {
        _providers.Remove(instance);
    }

}