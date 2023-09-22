namespace Godot.DependencyInjection.Attributes;

/// <summary>
/// Specifies that a field, property, or method should have its values provided.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
public sealed class ProvidedAttribute : Attribute
{
    /// <summary>
    /// Gets a value indicating whether the dependency is required.
    /// </summary>
    public bool IsRequired { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="InjectAttribute"/> class.
    /// </summary>
    /// <param name="isRequired">Indicates whether the dependency is required. Defaults to false.</param>
    public ProvidedAttribute(bool isRequired = false)
    {
        IsRequired = isRequired;
    }

}