namespace Godot.DependencyInjection.Core.UnitTests.FirstAssemblyToScan;

public class Provider : INodeProvider<Provider>
{
    public void Provide(out Provider node)
    {
        node = this;
    }
}