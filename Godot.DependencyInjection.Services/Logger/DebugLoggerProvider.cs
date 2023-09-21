using Microsoft.Extensions.Logging;

namespace Godot.DependencyInjection.Services.Logger;

/// <summary>
/// The provider for the <see cref="DebugLogger"/>.
/// </summary>
[ProviderAlias("Godot")]
public class DebugLoggerProvider : ILoggerProvider
{
    /// <inheritdoc />
    public ILogger CreateLogger(string name)
    {
        return new GodotLogger(name);
    }

    public void Dispose()
    {
    }
}