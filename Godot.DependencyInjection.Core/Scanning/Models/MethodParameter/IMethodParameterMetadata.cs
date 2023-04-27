namespace Godot.DependencyInjection.Scanning.Models.MethodParameterMetadata;

internal interface IMethodParameterMetadata
{
    object? GetService(IServiceProvider serviceProvider);
    string ToString();
    
    string DebugDisplay();
}
