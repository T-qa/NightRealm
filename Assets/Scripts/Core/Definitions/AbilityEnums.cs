/// <summary>
/// Ability and effect system enums
/// Consolidated definitions for effect types, ability responses, and targeting
/// </summary>

public enum EffectType
{
    PhysicsDamage,
    MagicDamage,
    Buff,
    Debuff,
    Heal,
    DamageOverTime
}

public enum Respond
{
    Success,
    NotEnoughMana,
    AnotherAbilityInUse,
    InvalidTarget,
    InCasting,
    InCooling,
    CanNotUse,
    NotAllow
}

public enum TargetType
{
    Ally,
    Enemy
}

public enum BonusType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMulti = 300
}
