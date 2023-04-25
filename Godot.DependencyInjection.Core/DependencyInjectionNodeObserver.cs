using Godot.DependencyInjection.Attributes;
using Godot.DependencyInjection.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Godot.DependencyInjection;

/// <summary>
/// Deprecated,use <see cref="DependencyInjectionManagerNode"/>
/// Represents an observer that handles dependency injection for Godot nodes.
/// </summary>
[IgnoreDependencyInjection]
[Obsolete("Use DependencyInjectionManagerNode instead of this class")]
public abstract partial class DependencyInjectionNodeObserver : DependencyInjectionManagerNode, IServicesConfigurator
{

    
    /// <inheritdoc/>
    public abstract void ConfigureServices(IServiceCollection services);

}


