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

public partial class RegularNode : Node2D
{
    [Inject]
    public IService service1;

    [Inject]
    public IService Service { get; set; }

    [Export]
    [InjectMembers]
    public CustomResource Resource { get; set; }

    [Inject]
    public void Inject(IService service)
    {
        // ***
    }
}
```


## Configuration

1. Create a script for dependency registration:
    ```csharp
    public partial class DependencyInjectionNode : DependencyInjectionNodeObserver
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IService, Service>();
        }
    }
    ```
2. Add the script to Project -> Project Settings -> Autoload.


## Remarks

Please note that this project is in its early stages of development and may require significant improvements. The framework is functional but may not be suitable for production use in its current state. Any feedback, suggestions, or contributions to improve the framework are highly appreciated.


## Support

If you have any questions, issues, or suggestions, please create a new issue on the GitHub repository.

## License
This project is licensed under the MIT License. See the  [LICENSE](license) file for more information.

