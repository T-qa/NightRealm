/// <summary>
/// Game-wide constants
/// Use these instead of magic numbers/strings throughout the project
/// </summary>
public static class GameConstants
{
    // Game settings
    public const string GAME_TITLE = "2D RPG Demo";
    public const float GAME_VERSION = 1.0f;

    // Gameplay
    public const float DEFAULT_GAME_SPEED = 1.0f;
    public const float MAX_GAME_SPEED = 2.0f;
    public const float MIN_GAME_SPEED = 0.1f;

    // Combat
    public const float DEFAULT_ATTACK_COOLDOWN = 1.0f;
    public const float DEFAULT_CAST_TIME = 0.5f;

    // Movement
    public const float DEFAULT_MOVE_SPEED = 5.0f;
    public const float DEFAULT_ACCELERATION = 10f;

    // UI
    public const float UI_ANIMATION_DURATION = 0.3f;
    public const float UI_TRANSITION_DURATION = 0.5f;

    // Audio
    public const float DEFAULT_MASTER_VOLUME = 0.8f;
    public const float DEFAULT_MUSIC_VOLUME = 0.6f;
    public const float DEFAULT_SFX_VOLUME = 0.7f;
}
