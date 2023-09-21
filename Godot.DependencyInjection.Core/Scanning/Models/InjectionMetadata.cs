using System.Diagnostics;
using Godot.DependencyInjection.Scanning.Models.Member;

namespace Godot.DependencyInjection.Scanning.Models;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal readonly struct InjectionMetadata
{
    private readonly IMemberMetadata[] _members;
    private readonly MethodMetadata[] _methods;
    private readonly NestedInjectionMetadata[] _nestedInjections;

    public InjectionMetadata(IMemberMetadata[] members, MethodMetadata[] methods, NestedInjectionMetadata[] nestedInjections)
    {
        _members = members;
        _methods = methods;
        _nestedInjections = nestedInjections;
    }

    public void Inject(IServiceProvider serviceProvider, object instance)
    {
        for (int i = 0; i < _members.Length; i++)
        {
            _members[i].Inject(serviceProvider, instance);
        }
        for (int i = 0; i < _methods.Length; i++)
        {
            _methods[i].Inject(serviceProvider, instance);
        }
    }

    public override string ToString()
    {
        return $@"
    {{
        ""members"":[{string.Join(",", (object?[]) _members)}
        ],
        ""methods"":[{string.Join(",", _methods)}
        ],
        ""nestedInjections"":[{string.Join(",", _nestedInjections)}
        ],
    }}";
    }
    internal string DebugDisplay()
    {
        return $@"members: {_members.Length}, methods: {_methods.Length}, nestedInjections: {_nestedInjections.Length}";
    }
}