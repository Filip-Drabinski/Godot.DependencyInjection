namespace Godot.DependencyInjection.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class InjectMembersAttribute : Attribute
{
    public InjectMembersAttribute()
    {
    }
}
