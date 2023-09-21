using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;

namespace Godot.DependencyInjection.Services.Logger;

/// <summary>
/// A logger that writes messages in the debug output window only when a debugger is attached.
/// </summary>
internal sealed class GodotLogger : ILogger
{
    private readonly string _name;

    /// <summary>
    /// Initializes a new instance of the <see cref="GodotLogger"/> class.
    /// </summary>
    /// <param name="name">The name of the logger.</param>
    public GodotLogger(string name)
    {
        _name = name;
    }

    /// <inheritdoc />
    public bool IsEnabled(LogLevel logLevel)
    {
        // If the filter is null, everything is enabled
        return logLevel != LogLevel.None;
    }

    /// <inheritdoc />
    public IDisposable BeginScope<TState>(TState state)
    {
        return NullScope.Instance;
    }


    /// <inheritdoc />
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }
        ArgumentNullException.ThrowIfNull(formatter);

        var message = formatter(state, exception);

        if (string.IsNullOrEmpty(message))
        {
            return;
        }

        message = $"{logLevel}: {_name}: {message}";

        if (exception != null)
        {
            message += System.Environment.NewLine + System.Environment.NewLine + exception;
        }
        PrintMessage(logLevel, message);
    }
    private static void PrintMessage(LogLevel logLevel, string message)
    {
        if (logLevel is LogLevel.Error or LogLevel.Critical)
            GD.PrintErr(message);
        else
            GD.Print(message);
    }
}