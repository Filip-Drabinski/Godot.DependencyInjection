using Godot.DependencyInjection.Scanning.Models.MethodParameterMetadata;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Godot.DependencyInjection.Scanning.Models.MethodParameter;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal struct MethodParameterMetadata : IMethodParameterMetadata
{
    public bool isRequired;
    public Type parameterType;
    public MethodParameterMetadata(bool isRequired, Type parameterType)
    {
        this.isRequired = isRequired;
        this.parameterType = parameterType;
    }
    public object? GetService(IServiceProvider serviceProvider)
    {
        var service = isRequired
        ? serviceProvider.GetRequiredService(parameterType)
        : serviceProvider.GetService(parameterType);

        return service;
    }


    public override string ToString()
    {
        return $@"
                    {{
                        ""type"": ""{parameterType.FullName}"",
                        ""isRequired"": {isRequired.ToString().ToLower()}
                    }}";
    }
    public string DebugDisplay()
    {
        return $@"{parameterType.FullName}({isRequired})";
    }
}
