using Godot.DependencyInjection.Core.UnitTests.FirstAssemblyToScan;
using Godot.DependencyInjection.Core.UnitTests.SecondAssemblyToScan;
using Godot.DependencyInjection.Scanning;

namespace Godot.DependencyInjection.Core.UnitTests;

public class InjectionScannerUnitTest
{
    [Fact]
    public void InjectionScanner_CollectMetadata_ShouldNotBeNull()
    {
        var assemblies = new[]
        {
            typeof(FirstAssembly1).Assembly, 
            typeof(SecondAssembly1).Assembly,
        };
        var res = InjectionScanner.CollectMetadata(assemblies);

        res.Should().NotBeNull();
    }
}