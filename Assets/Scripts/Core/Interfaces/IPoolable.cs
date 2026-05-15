/// <summary>
/// Interface for poolable objects
/// Moved to centralize object pooling contract
/// </summary>
public interface IPoolable
{
    void OnSpawn();
    void OnDespawn();
    void ResetState();
}
