using Godot.DependencyInjection.Scanning.Models.Shared;
using System.Diagnostics;

namespace Godot.DependencyInjection.Scanning.Models.MethodParameterMetadata;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal struct MethodEnumerableParameterMetadata : IMethodParameterMetadata
{
    public Type parameterType;

    public MethodEnumerableParameterMetadata(Type parameterType)
    {
        this.parameterType = parameterType;
    }

    public object? GetService(IServiceProvider serviceProvider)
    {
        var services = serviceProvider.GetServicesEnumerable(parameterType);
        return services;
    }

    public override string ToString()
    {
        return $@"
                    {{
                        ""type"": ""System.Collections.Generic.IEnumerable<{parameterType.FullName}>"",
                    }}";
    }

    public string DebugDisplay()
    {
        return $@"System.Collections.Generic.IEnumerable<{parameterType.FullName}>";
    }
}
