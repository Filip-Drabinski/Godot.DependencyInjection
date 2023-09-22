using Godot.DependencyInjection.Scanning.Models.Shared;
using System.Diagnostics;

namespace Godot.DependencyInjection.Scanning.Models.MethodParameterMetadata;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal readonly struct MethodEnumerableParameterMetadata : IMethodParameterMetadata
{
    private readonly Type _parameterType;
    private readonly bool _isProvided;

    public MethodEnumerableParameterMetadata(Type parameterType, bool isProvided)
    {
        _parameterType = parameterType;
        _isProvided = isProvided;
    }

    public object? GetService(IServiceProvider serviceProvider)
    {
        var services = _isProvided 
            ? serviceProvider.GetProvidedEnumerable(_parameterType) 
            : serviceProvider.GetServicesEnumerable(_parameterType);
        return services;
    }

    public override string ToString()
    {
        return $@"
                    {{
                        ""type"": ""System.Collections.Generic.IEnumerable<{_parameterType.FullName}>"",
                    }}";
    }

    public string DebugDisplay()
    {
        return $@"System.Collections.Generic.IEnumerable<{_parameterType.FullName}>";
    }
}
