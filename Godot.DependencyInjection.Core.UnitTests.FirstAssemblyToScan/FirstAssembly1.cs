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
    public string GuidPublic { get; set; }
    [InjectMembers]
    public FirstAssembly2 FirstAssembly2Public { get; set; } = new ();

    [Inject]
    protected string GuidProtected { get; set; }
    [InjectMembers]
    protected FirstAssembly2 FirstAssembly2Protected { get; set; } = new();
    [InjectMembers]

    [Inject]
    private string GuidPrivate { get; set; }
    [InjectMembers]
    private FirstAssembly2 FirstAssembly2Private { get; set; } = new();

    [Inject]
    public string guidPublic;

    [Inject]
    protected string guidProtected;

    [Inject]
    private string guidPrivate;


    [Inject]
    public void InjectPublic(string guid)
    {
    }
    [Inject]
    protected void InjectProtected(string guid)
    {
    }
    [Inject]
    private void InjectPrivate(string guid)
    {
    }
}