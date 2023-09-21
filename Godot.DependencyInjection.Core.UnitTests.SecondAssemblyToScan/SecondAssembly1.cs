using Godot.DependencyInjection.Attributes;

namespace Godot.DependencyInjection.Core.UnitTests.SecondAssemblyToScan;
public class SecondAssembly1
{
    [Inject]
    public Guid Guid { get; set; }

    [InjectMembers]
    public SecondAssembly2 SecondAssembly2 { get; set; } = null!;

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