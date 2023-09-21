using Godot.DependencyInjection.Scanning.Models.Shared;
using System.Diagnostics;

namespace Godot.DependencyInjection.Scanning.Models.MethodParameterMetadata;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal readonly struct MethodArrayParameterMetadata : IMethodParameterMetadata
{
    private readonly Type _parameterType;

    public MethodArrayParameterMetadata(Type parameterType)
    {
        _parameterType = parameterType;
    }

    public object? GetService(IServiceProvider serviceProvider)
    {
        var services = serviceProvider.GetServicesArray(_parameterType);
        return services;
    }

    public override string ToString()
    {
        return $@"
                    {{
                        ""type"": ""{_parameterType.FullName}[]"",
                    }}";
    }

    public string DebugDisplay()
    {
        return $@"{_parameterType.FullName}[]";
    }
}