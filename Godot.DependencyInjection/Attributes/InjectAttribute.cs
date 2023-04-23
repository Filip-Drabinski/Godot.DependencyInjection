namespace Godot.DependencyInjection.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class InjectAttribute : Attribute
{
    public bool IsRequired { get; }
    public InjectAttribute(bool isRequired = false)
    {
        IsRequired = isRequired;
    }

}

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public sealed class IgnoreDependencyInjectionAttribute : Attribute
{
    public IgnoreDependencyInjectionAttribute()
    {
    }

}
