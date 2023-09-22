using Godot.DependencyInjection.Attributes;

namespace Godot.DependencyInjection.Core.UnitTests.SecondAssemblyToScan;
public class SecondAssembly1
{
    [Inject]
    public string Guid { get; set; }
}