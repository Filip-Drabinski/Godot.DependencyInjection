using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Godot.DependencyInjection.Scanning.Models.Member;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal readonly struct MemberMetadata : IMemberMetadata
{
    public delegate void MemberSetter(object? instance, object? value);

    private readonly Type _serviceType;
    private readonly MemberSetter _memberSetter;
    private readonly bool _isRequired;

    public MemberMetadata(Type serviceType, MemberSetter memberSetter, bool isRequired)
    {
        _serviceType = serviceType;
        _memberSetter = memberSetter;
        _isRequired = isRequired;
    }
    
    /// <inheritdoc/>
    public void Inject(IServiceProvider serviceProvider, object instance)
    {
        var service = _isRequired
            ? serviceProvider.GetRequiredService(_serviceType)
            : serviceProvider.GetService(_serviceType);
        _memberSetter.Invoke(instance, service);
    }

    public override string ToString()
    {
        return $@"
            {{
                ""type"": ""{_serviceType.FullName}"",
                ""isRequired"": {_isRequired.ToString().ToLower()}
            }}";
    }
    
    /// <inheritdoc/>
    public string DebugDisplay()
    {
        return $@"{_serviceType.FullName}, isRequired: {_isRequired}";
    }

}
