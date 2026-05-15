using UnityEngine;

/// <summary>
/// Interface for objects that can take damage
/// Implemented by characters, enemies, and destructible objects
/// </summary>
public interface IDamageable
{
    void TakeDamage(DamageBlock damage);
    bool IsAlive();
    float GetHealthPercent();
}
