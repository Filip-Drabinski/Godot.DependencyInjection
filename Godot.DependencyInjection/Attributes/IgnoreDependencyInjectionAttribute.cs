namespace Godot.DependencyInjection.Attributes;

/// <summary>
/// Specifies that a class should be ignored for dependency injection.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public sealed class IgnoreDependencyInjectionAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IgnoreDependencyInjectionAttribute"/> class.
    /// </summary>
    public IgnoreDependencyInjectionAttribute()
    {
    }

}
