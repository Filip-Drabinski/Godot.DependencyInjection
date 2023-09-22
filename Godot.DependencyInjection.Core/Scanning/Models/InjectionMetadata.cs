using System.Diagnostics;
using Godot.DependencyInjection.Scanning.Models.Member;
using Godot.DependencyInjection.Scanning.Models.Provider;

namespace Godot.DependencyInjection.Scanning.Models;

[DebuggerDisplay("{DebugDisplay(),nq}")]
internal readonly struct InjectionMetadata
{
    internal readonly IMemberMetadata[] members;
    internal readonly MethodMetadata[] methods;
    internal readonly NestedInjectionMetadata[] nestedInjections;
    internal readonly IProviderMetadata[] providersMetadata;

    public InjectionMetadata(IMemberMetadata[] members, MethodMetadata[] methods,
        NestedInjectionMetadata[] nestedInjections, IProviderMetadata[] providersMetadata)
    {
        this.members = members;
        this.methods = methods;
        this.nestedInjections = nestedInjections;
        this.providersMetadata = providersMetadata;
    }

    public void Inject(IServiceProvider serviceProvider, object instance)
    {
        for (var i = 0; i < members.Length; i++)
        {
            members[i].Inject(serviceProvider, instance);
        }
        for (var i = 0; i < methods.Length; i++)
        {
            methods[i].Inject(serviceProvider, instance);
        }
    }

    public void AddProviders(IServiceProvider serviceProvider, object instance)
    {
        for (var i = 0; i < providersMetadata.Length; i++)
        {
            providersMetadata[i].Add(serviceProvider, instance);
        }
    }
    public void RemoveProviders(IServiceProvider serviceProvider, object instance)
    {
        for (var i = 0; i < providersMetadata.Length; i++)
        {
            providersMetadata[i].Remove(serviceProvider, instance);
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
        return $@"members: {members.Length}, methods: {methods.Length}, providing:{providersMetadata.Length}, nestedInjections: {nestedInjections.Length}";
    }
}