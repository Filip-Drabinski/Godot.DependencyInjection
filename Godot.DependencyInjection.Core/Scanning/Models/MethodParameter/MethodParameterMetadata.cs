using Godot.DependencyInjection.Scanning.Models.MethodParameterMetadata;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Godot.DependencyInjection.Scanning.Models.Shared;

namespace Godot.DependencyInjection.Scanning.Models.MethodParameter;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal readonly struct MethodParameterMetadata : IMethodParameterMetadata
{
    private readonly bool _isRequired;
    private readonly bool _isProvided;
    private readonly Type _parameterType;
    public MethodParameterMetadata(Type parameterType, bool isRequired, bool isProvided)
    {
        this._isRequired = isRequired;
        _isProvided = isProvided;
        this._parameterType = parameterType;
    }
    public object? GetService(IServiceProvider serviceProvider)
    {
        var service = _isProvided
            ? GetProvidedService(serviceProvider)
            : GetRegisteredService(serviceProvider);

        return service;
    }

    private object? GetProvidedService(IServiceProvider serviceProvider)
    {
        return _isRequired
            ? serviceProvider.GetRequiredProvided(_parameterType)
            : serviceProvider.GetProvided(_parameterType);
    }
    private object? GetRegisteredService(IServiceProvider serviceProvider)
    {
        return _isRequired
            ? serviceProvider.GetRequiredService(_parameterType)
            : serviceProvider.GetService(_parameterType);
    }


    public override string ToString()
    {
        return $@"
                    {{
                        ""type"": ""{_parameterType.FullName}"",
                        ""isRequired"": {_isRequired.ToString().ToLower()}
                    }}";
    }
    public string DebugDisplay()
    {
        return $@"{_parameterType.FullName}({_isRequired})";
    }
}
