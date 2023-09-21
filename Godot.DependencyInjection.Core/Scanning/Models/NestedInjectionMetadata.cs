using System.Diagnostics;

namespace Godot.DependencyInjection.Scanning.Models;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal readonly struct NestedInjectionMetadata
{
    public delegate object? MemberGetter(object? instance);
    internal readonly Type nestedType;
    internal readonly MemberGetter memberGetter;

    public NestedInjectionMetadata(Type nestedType, MemberGetter memberGetter)
    {
        this.nestedType = nestedType;
        this.memberGetter = memberGetter;
    }
    public override string ToString()
    {
        return $@"
            ""{nestedType.FullName}""";
    }
    internal string DebugDisplay()
    {
        return nestedType.FullName!;
    }
}