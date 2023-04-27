using Godot.DependencyInjection.Scanning.Models.Shared;
using System.Diagnostics;
using static Godot.DependencyInjection.Scanning.Models.Member.MemberMetadata;

namespace Godot.DependencyInjection.Scanning.Models.Member;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal struct MemberArrayMetadata : IMemberMetadata
{
    public Type serviceType;
    public MemberSetter memberSetter;

    public MemberArrayMetadata(Type serviceType, MemberSetter memberSetter)
    {
        this.serviceType = serviceType;
        this.memberSetter = memberSetter;
    }

    /// <inheritdoc/>
    public void Inject(IServiceProvider serviceProvider, object instance)
    {
        var services = serviceProvider.GetServicesArray(serviceType);
        memberSetter.Invoke(instance, services);
    }

    public override string ToString()
    {
        return $@"
            {{
                ""type"": ""{serviceType.FullName}[]"",
            }}";
    }

    /// <inheritdoc/>
    public string DebugDisplay()
    {
        return $@"{serviceType.FullName}[]";
    }

}