using System.Linq;
using Godot.DependencyInjection.Core.UnitTests.FirstAssemblyToScan;
using Godot.DependencyInjection.Core.UnitTests.SecondAssemblyToScan;
using Godot.DependencyInjection.Scanning;
using Godot.DependencyInjection.Scanning.Models.Provider;

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
        var metadata = InjectionScanner.CollectMetadata(assemblies);

        metadata.Should().NotBeNull();
    }

    [Fact]
    public void InjectionScanner_CollectMetadata_AllAssembliesScanned()
    {
        var assemblies = new[]
        {
            typeof(FirstAssembly1).Assembly, 
            typeof(SecondAssembly1).Assembly,
        };

        var metadata = InjectionScanner.CollectMetadata(assemblies);

        metadata.ContainsKey(typeof(FirstAssembly1)).Should().Be(true);
        metadata.ContainsKey(typeof(SecondAssembly1)).Should().Be(true);
    }
    [Fact]
    public void InjectionScanner_CollectMetadata_AnyAccessibilityCollected()
    {
        var assemblies = new[]
        {
            typeof(FirstAssembly1).Assembly, 
        };

        var metadata = InjectionScanner.CollectMetadata(assemblies);
        var typeMetadata = metadata[typeof(FirstAssembly1)];

        typeMetadata.members.Should().HaveCount(6);
        typeMetadata.methods.Should().HaveCount(3);
        typeMetadata.nestedInjections.Should().HaveCount(3);
    }
    [Fact]
    public void InjectionScanner_CollectMetadata_ProvidersCollected()
    {
        var assemblies = new[]
        {
            typeof(FirstAssembly1).Assembly, 
        };

        var metadata = InjectionScanner.CollectMetadata(assemblies);

        metadata.ContainsKey(typeof(Provider)).Should().BeTrue();
    }
    [Fact]
    public void InjectionScanner_CollectMetadata_CorrectProviderMetadataGenericCreated()
    {
        var assemblies = new[]
        {
            typeof(FirstAssembly1).Assembly, 
        };

        var metadata = InjectionScanner.CollectMetadata(assemblies);

        var injectionMetadata = metadata[typeof(Provider)];
        var providerType = injectionMetadata.providersMetadata[0].GetType();
        providerType.Should().Be(typeof(ProviderMetadata<INodeProvider<Provider>, Provider>));
    }
}