using UnityEngine;

/// <summary>
/// Layer name constants - prevents using strings like "Player" scattered throughout code
/// Usage: if (gameObject.layer == LayerConstant.PLAYER) { }
/// </summary>
public static class LayerConstants
{
    private static readonly int _player = LayerMask.NameToLayer("Player");
    private static readonly int _enemy = LayerMask.NameToLayer("Enemy");
    private static readonly int _ground = LayerMask.NameToLayer("Ground");
    private static readonly int _wall = LayerMask.NameToLayer("Wall");
    private static readonly int _interactable = LayerMask.NameToLayer("Interactable");
    private static readonly int _projectile = LayerMask.NameToLayer("Projectile");

    public static int PLAYER => _player;
    public static int ENEMY => _enemy;
    public static int GROUND => _ground;
    public static int WALL => _wall;
    public static int INTERACTABLE => _interactable;
    public static int PROJECTILE => _projectile;
}
