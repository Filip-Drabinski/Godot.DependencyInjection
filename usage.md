# Dependency Injection
## standard injection:
```csharp
using Godot.DependencyInjection.Services.Input;

public class RegularNode
{
    [Inject]
    public IInputService InputServicePublic { get; set; }

    [Inject]
    protected IInputService inputServiceProtected;

    [Inject]
    private IInputService _inputServicePrivate;

    [Inject]
    private void InjectPrivate(IInputService inputService)
    {
    }
    // ***
}
```
## nested injection:
```csharp
//RegularNode.cs
using Godot;
using Godot.DependencyInjection.Attributes;
using Godot.DependencyInjection.Services.Input;

public class RegularNode: Node
{
    [Inject]
    private IInputService _inputService;

    [Export]
    [InjectMembers]
    public CustomResource Resource { get; set; }
    // ***
}
```
```csharp
//CustomResource.cs
using Godot.DependencyInjection.Attributes;
using Godot.DependencyInjection.Services.Input;

public class CustomResource
{
    [Inject]
    public IService Service { get; set; }
    // ***
}
```

# node providers
```csharp
//RegularNode.cs
using Godot;
using Godot.DependencyInjection;

public class RegularNode: Node, IRegularNode, INodeProvider<LoggerTest>
{
    [Provided]
    private SecondNode _secondNode; 
    // ***
    public void Provide(out RegularNode node)
    {
        node = this;
    }
}
```
```csharp
//SecondNode.cs
using Godot;
using Godot.DependencyInjection;

public class SecondNode: Node, INodeProvider<SecondNode>
{
    [Provided]
    private RegularNode _regularNode;
    // ***
    public void Provide(out SecondNode node)
    {
        node = this;
    }
}
```
* Generic parameter of INodeProvider can be anything