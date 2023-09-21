using Godot.DependencyInjection.Scanning.Models.Shared;
using System.Diagnostics;
using static Godot.DependencyInjection.Scanning.Models.Member.MemberMetadata;

namespace Godot.DependencyInjection.Scanning.Models.Member;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal readonly struct MemberArrayMetadata : IMemberMetadata
{
    private readonly Type _serviceType;
    private readonly MemberSetter _memberSetter;

    public MemberArrayMetadata(Type serviceType, MemberSetter memberSetter)
    {
        _serviceType = serviceType;
        _memberSetter = memberSetter;
    }

    /// <inheritdoc/>
    public void Inject(IServiceProvider serviceProvider, object instance)
    {
        var services = serviceProvider.GetServicesArray(_serviceType);
        _memberSetter.Invoke(instance, services);
    }

    public override string ToString()
    {
        return $@"
            {{
                ""type"": ""{_serviceType.FullName}[]"",
            }}";
    }

    /// <inheritdoc/>
    public string DebugDisplay()
    {
        return $@"{_serviceType.FullName}[]";
    }

}