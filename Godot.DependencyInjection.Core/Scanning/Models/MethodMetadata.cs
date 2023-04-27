using Godot.DependencyInjection.Scanning.Models.MethodParameterMetadata;
using System.Diagnostics;
using System.Reflection;

namespace Godot.DependencyInjection.Scanning.Models;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal struct MethodMetadata
{
    public MethodInfo methodInfo;
    public IMethodParameterMetadata[] parameters;

    public MethodMetadata(MethodInfo methodInfo, IMethodParameterMetadata[] parameters)
    {
        this.methodInfo = methodInfo;
        this.parameters = parameters;
    }

    internal void Inject(IServiceProvider serviceProvider, object instance)
    {
        object?[] parametersValue = new object?[parameters.Length];
        for (int i = 0; i < parameters.Length; i++)
        {
            parametersValue[i] = parameters[i].GetService(serviceProvider);
        }
        methodInfo.Invoke(instance, parametersValue);
    }
    public override string ToString()
    {
        return $@"
            {{
                ""type"": ""{methodInfo.Name}"",
                ""parameters"": [{string.Join(',', (object?[]) parameters)}
                ]
            }}";
    }
    internal string DebugDisplay()
    {
        return $@"{methodInfo.Name}({string.Join(", ", parameters.Select(x => x.DebugDisplay()))})";
    }
}