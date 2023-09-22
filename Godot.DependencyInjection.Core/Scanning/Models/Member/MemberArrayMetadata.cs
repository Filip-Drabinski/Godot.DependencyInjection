using Godot.DependencyInjection.Scanning.Models.Shared;
using System.Diagnostics;
using static Godot.DependencyInjection.Scanning.Models.Member.MemberMetadata;

namespace Godot.DependencyInjection.Scanning.Models.Member;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal readonly struct MemberArrayMetadata : IMemberMetadata
{
    private readonly Type _serviceType;
    private readonly MemberSetter _memberSetter;
    private readonly bool _isProvided;

    public MemberArrayMetadata(Type serviceType, MemberSetter memberSetter, bool isProvided)
    {
        _serviceType = serviceType;
        _memberSetter = memberSetter;
        _isProvided = isProvided;
    }

    /// <inheritdoc/>
    public void Inject(IServiceProvider serviceProvider, object instance)
    {
        var services =_isProvided
            ? serviceProvider.GetProvidedArray(_serviceType) 
            : serviceProvider.GetServicesArray(_serviceType);
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