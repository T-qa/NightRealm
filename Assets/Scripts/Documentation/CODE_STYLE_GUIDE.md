# Code Style Guide

## C# Naming Conventions

### Classes and Types
```csharp
// Classes: PascalCase
public class PlayerController { }
public struct Vector3 { }
public enum DamageType { }

// Interfaces: I prefix + PascalCase
public interface IDamageable { }
public interface IInteractable { }

// Methods: PascalCase
public void TakeDamage() { }
public bool IsAlive() { }

// Properties: PascalCase
public float Health { get; set; }
public string Name { get; private set; }

// Fields: camelCase with _ prefix for private
private float _maxHealth;
private int _damageDealt;

// Local variables: camelCase
int localVariable = 10;
bool isMoving = true;

// Constants: UPPER_SNAKE_CASE
private const float MAX_SPEED = 10f;
public const string PLAYER_TAG = "Player";
```

### Parameters and Local Variables
```csharp
// All use camelCase
public void Setup(int maxHealth, float attackPower)
{
    int damageValue = 10;
    bool canAttack = true;
}
```

---

## File Organization

### File Structure
```csharp
// 1. Using statements
using System;
using UnityEngine;

// 2. Namespace (if applicable)
namespace Tqa.DungeonQuest.Character
{
    // 3. Class declaration
    public class PlayerController : BaseController
    {
        // 4. Nested types (if needed)
        public enum State { Idle, Running, Jumping }

        // 5. Static fields and properties
        private static PlayerController _instance;

        // 6. Constants
        private const float MAX_SPEED = 10f;

        // 7. Serialized fields
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private Animator _animator;

        // 8. Private fields
        private int _currentHealth;
        private bool _isJumping;

        // 9. Public properties
        public bool IsAlive { get; private set; }
        public float HealthPercent => _currentHealth / _maxHealth;

        // 10. Unity lifecycle methods
        private void Start() { }
        private void Update() { }
        private void LateUpdate() { }
        private void OnEnable() { }
        private void OnDisable() { }

        // 11. Public methods
        public void TakeDamage(int amount) { }
        public void Heal(int amount) { }

        // 12. Private methods
        private void UpdateAnimation() { }
        private void CheckInput() { }
    }
}
```

### Region Organization (Optional but Recommended)
```csharp
public class CharacterController : BaseController
{
    #region Fields
    private float _health;
    private float _maxHealth;
    #endregion

    #region Properties
    public bool IsAlive { get; private set; }
    #endregion

    #region Lifecycle
    private void Start() { }
    private void Update() { }
    #endregion

    #region Public Methods
    public void TakeDamage(float amount) { }
    #endregion

    #region Private Methods
    private void UpdateHealth() { }
    #endregion
}
```

---

## Documentation and Comments

### XML Documentation Comments
```csharp
/// <summary>
/// Sets the character's health to a specific value.
/// Clamps the value between 0 and max health.
/// </summary>
/// <param name="newHealth">The new health value</param>
public void SetHealth(float newHealth)
{
    _health = Mathf.Clamp(newHealth, 0, _maxHealth);
}

/// <summary>
/// Applies damage to this character.
/// </summary>
/// <param name="damageBlock">The damage block containing damage information</param>
/// <returns>True if damage was applied, false if blocked</returns>
public bool TakeDamage(DamageBlock damageBlock)
{
    // Implementation
    return true;
}
```

### Inline Comments
```csharp
// Explain WHY, not WHAT
// Bad: Increment health
currentHealth++;

// Good: Cap health at max to prevent overflow
currentHealth = Mathf.Min(currentHealth + 1, maxHealth);

// For complex logic, explain the reasoning
// We check for null here because the ability might not be loaded yet
if (selectedAbility != null)
{
    selectedAbility.Execute();
}
```

---

## Formatting and Layout

### Spacing
```csharp
// One blank line between methods/properties
public void Method1() { }

public void Method2() { }

// No blank lines between properties of same type
public int Health { get; set; }
public int Mana { get; set; }
```

### Indentation
- Use 4 spaces (not tabs)
- Consistent indentation throughout

### Line Length
- Aim for 100-120 characters max
- Break long lines logically

```csharp
// Long method call broken into multiple lines
var result = SomeComplexCalculation(
    parameter1,
    parameter2,
    parameter3
);

// Long conditional split logically
if (character.IsAlive &&
    character.IsReady &&
    character.HasMana)
{
    character.CastAbility();
}
```

---

## Access Modifiers

### Default to Private
```csharp
// Only expose what needs to be exposed
private float _health;           // Private by default
public float HealthPercent { get; private set; }  // Limited access

// Not: public float _health;
```

### Use Properties, Not Public Fields
```csharp
// Good
public float Health { get; private set; }

// Bad
public float _health;
```

---

## Design Principles

### SOLID Principles

**S - Single Responsibility**
```csharp
// Bad: PlayerController does too much
public class PlayerController : MonoBehaviour
{
    public void TakeDamage() { }
    public void UpdateUI() { }
    public void PlaySound() { }
}

// Good: Separated concerns
public class PlayerController : BaseController, IDamageable { }
public class PlayerHealthUI : MonoBehaviour { }
public class AudioManager : Singleton<AudioManager> { }
```

**O - Open/Closed**
```csharp
// Open for extension, closed for modification
public abstract class BaseAbility : MonoBehaviour
{
    public abstract void Execute();
}

public class FireballAbility : BaseAbility
{
    public override void Execute() { }
}
```

**L - Liskov Substitution**
```csharp
// Subtypes must be substitutable
public interface IDamageable
{
    void TakeDamage(DamageBlock damage);
}

// Both can be used where IDamageable is expected
public class Character : IDamageable { }
public class Destructible : IDamageable { }
```

**I - Interface Segregation**
```csharp
// Specific interfaces, not one large interface
public interface IDamageable { void TakeDamage(DamageBlock damage); }
public interface IHealer { void Heal(float amount); }

// Rather than:
// public interface IGeneric { void TakeDamage(); void Heal(); }
```

**D - Dependency Inversion**
```csharp
// Depend on abstractions, not concrete classes
public class EnemyController : BaseController
{
    private IDamageable _target;  // Interface, not Enemy class
    
    public void SetTarget(IDamageable target)
    {
        _target = target;
    }
}
```

---

## Common Patterns

### Null Checking
```csharp
// Modern C# style
if (obj is null) { }      // Preferred
if (obj == null) { }      // Also fine
if (!obj) { }             // Less clear

// With Properties
if (string.IsNullOrEmpty(name)) { }
if (collection?.Count > 0) { }  // Null-conditional operator
```

### Loops
```csharp
// Prefer foreach
foreach (var item in items)
{
    ProcessItem(item);
}

// When you need index
for (int i = 0; i < items.Count; i++)
{
    ProcessItem(items[i], i);
}

// Avoid
while (i < items.Count)
{
    // Harder to maintain
}
```

### LINQ Usage
```csharp
// Good use of LINQ
var activePlayers = players.Where(p => p.IsActive).ToList();
var totalDamage = abilities.Sum(a => a.Damage);
var groups = items.GroupBy(i => i.Rarity);

// Avoid over-complex LINQ chains
// Break into multiple lines or separate methods
var result = data
    .Where(x => x.IsValid)
    .GroupBy(x => x.Category)
    .Select(g => new { g.Key, Count = g.Count() })
    .ToList();
```

---

## Performance Considerations

### Caching
```csharp
public class CharacterController : BaseController
{
    private Animator _animator;              // Cached
    private Rigidbody2D _rigidbody;          // Cached
    private int _moveSpeedHash;              // Cache animator hash

    private void Start()
    {
        // Cache references once in Start
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveSpeedHash = Animator.StringToHash("MoveSpeed");
    }

    private void Update()
    {
        // Use cached hash instead of string
        _animator.SetInteger(_moveSpeedHash, moveSpeed);
    }
}
```

### Collection Usage
```csharp
// Don't create new lists in loops
// Bad
foreach (var enemy in allEnemies)
{
    var nearEnemies = GetNearbyEnemies(enemy.Position);  // Creates new list each frame
    AttackAll(nearEnemies);
}

// Good
var nearbyCache = new List<Enemy>();
foreach (var enemy in allEnemies)
{
    nearbyCache.Clear();
    GetNearbyEnemies(enemy.Position, nearbyCache);
    AttackAll(nearbyCache);
}
```

---

## Error Handling

### Use Assertions for Validation
```csharp
public void Initialize(Character character)
{
    // Fail fast in development
    Debug.Assert(character != null, "Character cannot be null!");
    
    if (character == null)
    {
        Debug.LogError("Attempted to initialize with null character");
        return;
    }
}
```

### Meaningful Error Messages
```csharp
// Bad
Debug.LogError("Error");

// Good
Debug.LogError($"Failed to load ability '{abilityName}'. Make sure the ID exists in the database.");
```

---

## Testing Considerations

### Make Code Testable
```csharp
// Inject dependencies for testing
public class CombatSystem
{
    private IDamageCalculator _calculator;

    public CombatSystem(IDamageCalculator calculator)
    {
        _calculator = calculator;
    }

    public float CalculateDamage(Character attacker, Character defender)
    {
        return _calculator.Calculate(attacker, defender);
    }
}

// In tests, inject a mock calculator
```

---

## Git Commit Messages

```
// Format: [Type] Brief description

[FEATURE] Add boss phase system
[FIX] Fix health bar not updating
[REFACTOR] Reorganize UI system structure
[DOCS] Update architecture documentation
[PERF] Cache animator parameters for better performance
[STYLE] Remove trailing whitespace

// Keep descriptions concise (50 chars or less)
```

---

**Last Updated:** May 15, 2026  
**Version:** 1.0
