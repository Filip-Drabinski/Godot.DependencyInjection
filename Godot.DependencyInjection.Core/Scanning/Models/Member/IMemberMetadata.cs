namespace Godot.DependencyInjection.Scanning.Models.Member;

internal interface IMemberMetadata
{
    /// <summary>
    /// Injects dependency into instance
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="instance"></param>
    void Inject(IServiceProvider serviceProvider, object instance);

    /// <summary>
    /// Value used in <see cref="System.Diagnostics.DebuggerDisplayAttribute"/>
    /// </summary>
    /// <returns></returns>
    string DebugDisplay();
}
