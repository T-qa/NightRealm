/// <summary>
/// Core combat system enums
/// Consolidated definitions for damage types, fighter teams, and combat states
/// </summary>
/// 
public enum DamageState
{
    NormalDamage,
    CriticalDamage,
    BlockDamage,
    Miss
}

public enum DamageType
{
    PhysicalDamage,
    MagicDamage,
    EnvinronmentalDamage
}

public enum FighterTeam
{
    Hero,
    Monster
}
