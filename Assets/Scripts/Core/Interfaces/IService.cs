/// <summary>
/// Base interface for all game services
/// Services should be registered with ServiceLocator for global access
/// </summary>
public interface IService
{
    void Initialize();
    void Shutdown();
}
