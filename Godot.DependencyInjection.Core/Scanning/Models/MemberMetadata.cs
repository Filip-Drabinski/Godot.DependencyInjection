using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Godot.DependencyInjection.Scanning.Models;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal struct MemberMetadata
{
    public delegate void MemberSetter(object? instance, object? value);
    public Type serviceType;
    public MemberSetter memberSetter;
    public bool isRequired;

    public MemberMetadata(Type serviceType, MemberSetter memberSetter, bool isRequired)
    {
        this.serviceType = serviceType;
        this.memberSetter = memberSetter;
        this.isRequired = isRequired;
    }

    internal void Inject(IServiceProvider serviceProvider, object instance)
    {
        var service = isRequired
            ? serviceProvider.GetRequiredService(serviceType)
            : serviceProvider.GetService(serviceType);
        memberSetter.Invoke(instance, service);
    }

    public override string ToString()
    {
        return $@"
            {{
                ""type"": ""{serviceType.FullName}"",
                ""isRequired"": {isRequired.ToString().ToLower()}
            }}";
    }
    internal string DebugDisplay()
    {
        return $@"{serviceType.FullName}, isRequired: {isRequired}";
    }
}