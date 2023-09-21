using Godot.DependencyInjection.Scanning.Models.Shared;
using System.Diagnostics;

namespace Godot.DependencyInjection.Scanning.Models.MethodParameterMetadata;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal readonly struct MethodEnumerableParameterMetadata : IMethodParameterMetadata
{
    private readonly Type _parameterType;

    public MethodEnumerableParameterMetadata(Type parameterType)
    {
        _parameterType = parameterType;
    }

    public object? GetService(IServiceProvider serviceProvider)
    {
        var services = serviceProvider.GetServicesEnumerable(_parameterType);
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
