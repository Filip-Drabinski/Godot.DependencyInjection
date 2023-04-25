namespace Godot.DependencyInjection.Attributes;
/// <summary>
/// Specifies that members (fields, properties, or methods) of the target should have their dependencies injected.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class InjectMembersAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InjectMembersAttribute"/> class.
    /// </summary>
    public InjectMembersAttribute()
    {
    }
}
