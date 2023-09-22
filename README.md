# Godot.DependencyInjection

Godot.DependencyInjection is a lightweight and easy-to-use dependency injection framework for the [Godot](https://godotengine.org/) game engine, specifically tailored for C#. It aims to help developers create more modular, testable, and maintainable game projects using the Godot engine.


## Features

- Easy to set up and use
- Utilizes standard C# dependency injection abstractions
- Supports injection of: fields, properties and methods with any accessibility
- built-in provider abstraction for retrieving any node from scene
- Provides nested dependency injection for usage with resources
- Supports transient, scoped, and singleton lifetimes

## Installation

You can install the package via [NuGet](https://www.nuget.org/packages/Godot.DependencyInjection) or clone the repository and add a project reference to the solution created by Godot.


## Usage

```csharp
using Godot.DependencyInjection.Services.Input;

public partial class RegularNode : Node2D
{
    [Inject]
    private IInputService _inputService;
    // ***
}
```
[details](usage.md)

## Configuration

1. Create a script for dependency manager:
    ```csharp
    public partial class DependencyInjectionNode : DependencyInjectionManagerNode
    {
    }
    ```
2. Add the script to Project -> Project Settings -> Autoload.
3. Create a `Node` and script for dependency registration:
    ```csharp
    public partial class DependencyRegistrationNode : Node, IServicesConfigurator
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGodotServices();
            services.AddTransient<IService, Service>();
        }
    }
    ```
4. Add dependency registration node to scene

## Remarks

Please note that this project is in its early stages of development and may require significant improvements. The framework is functional but may not be suitable for production use in its current state. Any feedback, suggestions, or contributions to improve the framework are highly appreciated.

## Contributing

I'm currently in the process of reevaluating the architecture and scope of the project, so only pull requests containing changes to documentation might be merged. If you have any ideas for code improvements, please create a new issue instead.


## Support

If you have any questions, issues, or suggestions, please create a new issue on the GitHub repository.


## License
This project is licensed under the MIT License. See the  [LICENSE](license) file for more information.

