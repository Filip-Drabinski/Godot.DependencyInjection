using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot.DependencyInjection.Attributes;

namespace Godot.DependencyInjection.Core.UnitTests.FirstAssemblyToScan;

public class FirstAssembly1
{
    [Inject]
    public Guid Guid { get; set; }

    [InjectMembers]
    public FirstAssembly2 FirstAssembly2 { get; set; } = null!;

    public Guid guid;
    public Guid guid2;
    public Guid guid3;
    public void InjectPublic(Guid guid)
    {
        this.guid = guid;
    }
    protected void InjectProtected(Guid guid)
    {
        guid2 = guid;
    }
    private void InjectPrivate(Guid guid)
    {
        guid3 = guid;
    }
}