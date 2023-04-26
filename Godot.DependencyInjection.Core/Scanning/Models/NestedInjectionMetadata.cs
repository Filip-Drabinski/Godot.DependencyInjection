using System.Diagnostics;

namespace Godot.DependencyInjection.Scanning.Models;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal struct NestedInjectionMetadata
{
    public delegate object? MemberGetter(object? instance);
    public Type nestedType;
    public MemberGetter memberGetter;

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