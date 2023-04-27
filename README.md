# Godot.DependencyInjection

Godot.DependencyInjection is a lightweight and easy-to-use dependency injection framework for the [Godot](https://godotengine.org/) game engine, specifically tailored for C#. It aims to help developers create more modular, testable, and maintainable game projects using the Godot engine.


## Features

- Easy to set up and use
- Supports property, field, and method injection
- Provides member dependency injection for usage with resources
- Utilizes standard C# dependency injection abstractions
- Supports transient, scoped, and singleton lifetimes


## Installation

You can install the package via [NuGet](https://www.nuget.org/packages/Godot.DependencyInjection) or clone the repository and add a project reference to the solution created by Godot.


## Usage

```csharp
using Godot.DependencyInjection.Services.Input;

public partial class RegularNode : Node2D
{
    [Inject]
    public IInputService inputService;

    [Inject]
    public IService Service { get; set; }

    [Inject]
    public IService[] Services1 { get; set; }

    [Inject]
    public IEnumerable<IService> Services2 { get; set; }

    [Export]
    [InjectMembers]
    public CustomResource Resource { get; set; }

    [Inject]
    public void Inject(IService service, IService[] services1, IEnumerable<IService> services2)
    {
        // ***
    }
}
```


## Configuration

1. Create a script for dependency manager:
    ```csharp
    public partial class DependencyInjectionNode : DependencyInjectionManagerNode
    {
    }
    ```
2. Add the script to Project -> Project Settings -> Autoload.
3. Create a node and script for dependency registration:
    ```csharp
    public partial class DependencyRegistrationNode : IServicesConfigurator
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddGodotServices();
            services.AddTransient<IService, Service>();
        }
    }
    ```
4. Add dependency registration node to scene


## Remarks

Please note that this project is in its early stages of development and may require significant improvements. The framework is functional but may not be suitable for production use in its current state. Any feedback, suggestions, or contributions to improve the framework are highly appreciated.


## Support

If you have any questions, issues, or suggestions, please create a new issue on the GitHub repository.

## License
This project is licensed under the MIT License. See the  [LICENSE](license) file for more information.

