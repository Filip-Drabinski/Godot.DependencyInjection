using Godot.DependencyInjection.Attributes;

namespace Godot.DependencyInjection.Core.UnitTests.SecondAssemblyToScan;

public class SecondAssembly2
{
    [Inject]
    public Guid Guid { get; set; }
}