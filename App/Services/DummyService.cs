using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace App.Services;

public class DummyService : IDummyService
{
    private readonly ILogger _logger;

    public DummyService(ILogger logger)
    {
        _logger = logger;
    }

    public Task DoNothingAsync()
    {
        LogToAllLevels(nameof(DoNothingAsync));
        return Task.CompletedTask;
    }

    private void LogToAllLevels(string message)
    {
        var name = $"Scope-{GetType().Namespace}";
        using var scope = _logger.BeginScope(name);
        _logger.LogTrace("Received message: {Message}", message);
        _logger.LogDebug("Received message: {Message}", message);
        _logger.LogInformation("Received message: {Message}", message);
        _logger.LogWarning("Received message: {Message}", message);
        _logger.LogError("Received message: {Message}", message);
        _logger.LogCritical("Received message: {Message}", message);
    }
}