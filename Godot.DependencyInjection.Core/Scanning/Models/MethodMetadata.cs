using Godot.DependencyInjection.Scanning.Models.MethodParameterMetadata;
using System.Diagnostics;
using System.Reflection;

namespace Godot.DependencyInjection.Scanning.Models;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal readonly struct MethodMetadata
{
    private readonly MethodInfo _methodInfo;
    private readonly IMethodParameterMetadata[] _parameters;

    public MethodMetadata(MethodInfo methodInfo, IMethodParameterMetadata[] parameters)
    {
        _methodInfo = methodInfo;
        _parameters = parameters;
    }

    internal void Inject(IServiceProvider serviceProvider, object instance)
    {
        var parametersValue = new object?[_parameters.Length];
        for (var i = 0; i < _parameters.Length; i++)
        {
            parametersValue[i] = _parameters[i].GetService(serviceProvider);
        }
        _methodInfo.Invoke(instance, parametersValue);
    }
    public override string ToString()
    {
        return $@"
            {{
                ""type"": ""{_methodInfo.Name}"",
                ""parameters"": [{string.Join(',', (object?[]) _parameters)}
                ]
            }}";
    }
    internal string DebugDisplay()
    {
        return $@"{_methodInfo.Name}({string.Join(", ", _parameters.Select(x => x.DebugDisplay()))})";
    }
}