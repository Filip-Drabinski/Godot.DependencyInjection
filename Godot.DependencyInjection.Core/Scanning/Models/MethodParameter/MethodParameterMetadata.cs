using Godot.DependencyInjection.Scanning.Models.MethodParameterMetadata;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Godot.DependencyInjection.Scanning.Models.MethodParameter;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal readonly struct MethodParameterMetadata : IMethodParameterMetadata
{
    private readonly bool _isRequired;
    private readonly Type _parameterType;
    public MethodParameterMetadata(bool isRequired, Type parameterType)
    {
        this._isRequired = isRequired;
        this._parameterType = parameterType;
    }
    public object? GetService(IServiceProvider serviceProvider)
    {
        var service = _isRequired
        ? serviceProvider.GetRequiredService(_parameterType)
        : serviceProvider.GetService(_parameterType);

        return service;
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
