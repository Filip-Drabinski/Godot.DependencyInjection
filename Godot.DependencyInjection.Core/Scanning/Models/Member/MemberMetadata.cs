using Godot.DependencyInjection.Scanning.Models.Shared;
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
    private readonly bool _isProvided;

    public MemberMetadata(Type serviceType, MemberSetter memberSetter, bool isRequired, bool isProvided)
    {
        _serviceType = serviceType;
        _memberSetter = memberSetter;
        _isRequired = isRequired;
        _isProvided = isProvided;
    }

    /// <inheritdoc/>
    public void Inject(IServiceProvider serviceProvider, object instance)
    {
        var service = _isProvided 
            ? GetProvided(serviceProvider) 
            : GetService(serviceProvider);
        _memberSetter.Invoke(instance, service);
    }

    private object? GetService(IServiceProvider serviceProvider)
    {
        return _isRequired
            ? serviceProvider.GetRequiredService(_serviceType)
            : serviceProvider.GetService(_serviceType);
    }
    private object? GetProvided(IServiceProvider serviceProvider)
    {
        return _isRequired
            ? serviceProvider.GetRequiredProvided(_serviceType)
            : serviceProvider.GetProvided(_serviceType);
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
