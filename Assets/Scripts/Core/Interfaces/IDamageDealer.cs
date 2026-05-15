/// <summary>
/// Interface for objects that can deal damage
/// Implemented by weapons, abilities, and hazards
/// </summary>
public interface IDamageDealer
{
    DamageBlock CreateDamageBlock();
    void OnHit(IDamageable target);
}
