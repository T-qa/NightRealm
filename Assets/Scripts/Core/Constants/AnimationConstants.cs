using UnityEngine;

/// <summary>
/// Animation parameter hashes for optimal performance
/// Using hash instead of string prevents allocations and string comparisons
/// Usage: animator.SetInteger(AnimationConstants.SPEED, moveSpeed);
/// </summary>
public static class AnimationConstants
{
    // Common animations
    public static readonly int SPEED = Animator.StringToHash("Speed");
    public static readonly int IS_RUNNING = Animator.StringToHash("IsRunning");
    public static readonly int IS_JUMPING = Animator.StringToHash("IsJumping");
    public static readonly int IS_FALLING = Animator.StringToHash("IsFalling");
    public static readonly int IS_ATTACKING = Animator.StringToHash("IsAttacking");
    public static readonly int IS_CASTING = Animator.StringToHash("IsCasting");
    public static readonly int IS_DEAD = Animator.StringToHash("IsDead");
    public static readonly int ATTACK_SPEED = Animator.StringToHash("AttackSpeed");

    // Combat animations
    public static readonly int COMBO_INDEX = Animator.StringToHash("ComboIndex");
    public static readonly int ABILITY_INDEX = Animator.StringToHash("AbilityIndex");

    // Enemy animations
    public static readonly int DIRECTION = Animator.StringToHash("Direction");
    public static readonly int STATE = Animator.StringToHash("State");
}
