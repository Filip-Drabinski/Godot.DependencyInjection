using Godot.DependencyInjection.Attributes;

namespace Godot.DependencyInjection.Core.UnitTests.FirstAssemblyToScan;

public class FirstAssembly2
{
    [Inject]
    public Guid Guid { get; set; }
}