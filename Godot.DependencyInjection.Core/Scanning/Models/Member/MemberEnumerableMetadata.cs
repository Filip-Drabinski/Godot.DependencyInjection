using Godot.DependencyInjection.Scanning.Models.Shared;
using System.Diagnostics;
using static Godot.DependencyInjection.Scanning.Models.Member.MemberMetadata;

namespace Godot.DependencyInjection.Scanning.Models.Member;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal struct MemberEnumerableMetadata : IMemberMetadata
{
    public Type serviceType;
    public MemberSetter memberSetter;

    public MemberEnumerableMetadata(Type serviceType, MemberSetter memberSetter)
    {
        this.serviceType = serviceType;
        this.memberSetter = memberSetter;
    }

    /// <inheritdoc/>
    public void Inject(IServiceProvider serviceProvider, object instance)
    {
        var services = serviceProvider.GetServicesEnumerable(serviceType);
        memberSetter.Invoke(instance, services);
    }

    public override string ToString()
    {
        return $@"
            {{
                ""type"": ""System.Collections.Generic.IEnumerable<{serviceType.FullName}>"",
            }}";
    }

    /// <inheritdoc/>
    public string DebugDisplay()
    {
        return $@"System.Collections.Generic.IEnumerable<{serviceType.FullName}>";
    }

}
