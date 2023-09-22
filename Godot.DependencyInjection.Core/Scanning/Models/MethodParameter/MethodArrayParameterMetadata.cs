using Godot.DependencyInjection.Scanning.Models.Shared;
using System.Diagnostics;

namespace Godot.DependencyInjection.Scanning.Models.MethodParameterMetadata;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal readonly struct MethodArrayParameterMetadata : IMethodParameterMetadata
{
    private readonly Type _parameterType;
    private readonly bool _isProvided;

    public MethodArrayParameterMetadata(Type parameterType, bool isProvided)
    {
        _parameterType = parameterType;
        _isProvided = isProvided;
    }

    public object? GetService(IServiceProvider serviceProvider)
    {
        var services = _isProvided
            ? serviceProvider.GetProvidedArray(_parameterType)
            : serviceProvider.GetServicesArray(_parameterType);
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