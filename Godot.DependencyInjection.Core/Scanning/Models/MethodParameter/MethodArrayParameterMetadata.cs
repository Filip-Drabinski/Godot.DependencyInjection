using Godot.DependencyInjection.Scanning.Models.Shared;
using System.Diagnostics;

namespace Godot.DependencyInjection.Scanning.Models.MethodParameterMetadata;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal struct MethodArrayParameterMetadata : IMethodParameterMetadata
{
    public Type parameterType;

    public MethodArrayParameterMetadata(Type parameterType)
    {
        this.parameterType = parameterType;
    }

    public object? GetService(IServiceProvider serviceProvider)
    {
        var services = serviceProvider.GetServicesArray(parameterType);
        return services;
    }

    public override string ToString()
    {
        return $@"
                    {{
                        ""type"": ""{parameterType.FullName}[]"",
                    }}";
    }

    public string DebugDisplay()
    {
        return $@"{parameterType.FullName}[]";
    }
}