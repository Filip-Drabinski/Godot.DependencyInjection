using System.Diagnostics;
using Godot.DependencyInjection.Scanning.Models.Member;

namespace Godot.DependencyInjection.Scanning.Models;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal struct InjectionMetadata
{
    public IMemberMetadata[] members;
    public MethodMetadata[] methods;
    public NestedInjectionMetadata[] nestedInjections;

    public InjectionMetadata(IMemberMetadata[] members, MethodMetadata[] methods, NestedInjectionMetadata[] nestedInjections)
    {
        this.members = members;
        this.methods = methods;
        this.nestedInjections = nestedInjections;
    }

    public void Inject(IServiceProvider serviceProvider, object instance)
    {
        for (int i = 0; i < members.Length; i++)
        {
            members[i].Inject(serviceProvider, instance);
        }
        for (int i = 0; i < methods.Length; i++)
        {
            methods[i].Inject(serviceProvider, instance);
        }
    }

    public override string ToString()
    {
        return $@"
    {{
        ""members"":[{string.Join(",", (object?[]) members)}
        ],
        ""methods"":[{string.Join(",", methods)}
        ],
        ""nestedInjections"":[{string.Join(",", nestedInjections)}
        ],
    }}";
    }
    internal string DebugDisplay()
    {
        return $@"members: {members.Length}, methods: {methods.Length}, nestedInjections: {nestedInjections.Length}";
    }
}