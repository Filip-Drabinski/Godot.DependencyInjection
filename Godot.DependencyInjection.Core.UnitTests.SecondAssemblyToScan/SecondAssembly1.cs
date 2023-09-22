using Godot.DependencyInjection.Attributes;

namespace Godot.DependencyInjection.Core.UnitTests.SecondAssemblyToScan;
public class SecondAssembly1
{
    [Inject]
    public Guid Guid { get; set; }
}