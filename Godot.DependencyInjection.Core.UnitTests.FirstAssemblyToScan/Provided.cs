using Godot.DependencyInjection.Attributes;

namespace Godot.DependencyInjection.Core.UnitTests.FirstAssemblyToScan;

public class Provided
{
    [Provided]
    public Provider Value;
}