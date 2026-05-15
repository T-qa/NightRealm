# Design Patterns Used in This Project

Reference guide for common design patterns implemented in this project.

---

## 1. Singleton Pattern

### Purpose
Ensure a class has only one instance and provide a global point of access.

### MonoBehaviourSingleton (For MonoBehaviours)
```csharp
public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    public void PlaySound(string soundName) { }
}

// Usage from anywhere:
AudioManager.Instance.PlaySound("Footstep");
```

### Regular Singleton (For Non-MonoBehaviours)
```csharp
public class GameConfig : Singleton<GameConfig>
{
    public int MaxLevel { get; set; } = 50;
}

// Usage:
GameConfig.Instance.MaxLevel;
```

### When to Use
- Global game managers (AudioManager, UIManager, GameManager)
- Configuration holders
- Caches and registries

---

## 2. Service Locator Pattern

### Purpose
Provide a central location to access all game services without hard dependencies.

```csharp
// Register services
ServiceLocator.Instance.Register<AudioManager>(audioManager);
ServiceLocator.Instance.Register<UIManager>(uiManager);

// Use services
var audio = ServiceLocator.Instance.Get<AudioManager>();
audio.PlaySound("Explosion");
```

### Benefits
- Reduced coupling between systems
- Easy to swap implementations (testing)
- No need for hard references

### When to Use
- Accessing managers from anywhere
- Testing (swap implementations)
- Late initialization of systems

---

## 3. Factory Pattern

### Purpose
Create objects without specifying their exact classes.

```csharp
public interface IItemFactory
{
    Item CreateItem(ItemSpecification spec);
}

public class EquipmentFactory : MonoBehaviour, IItemFactory
{
    public Item CreateItem(ItemSpecification spec)
    {
        Equipment equipment = new Equipment();
        equipment.SetStats(spec.AttackPower, spec.Defense);
        return equipment;
    }
}

// Usage: Factories handle creation complexity
Item newSword = weaponFactory.CreateItem(swordSpec);
```

### Locations in Project
- `Items/Factory/` - Item creation
- `Enemy/Base/` (potential) - Enemy creation
- `Core/ScriptableObjects/` - Asset creation

### When to Use
- Complex object creation
- Multiple ways to create similar objects
- Need to switch creation logic

---

## 4. Observer/Event Pattern

### Purpose
Define one-to-many relationship where many objects observe one object.

```csharp
// Define event
public static class GameEvents
{
    public static event System.Action<DamageBlock> OnPlayerDamaged;
}

// Publisher: Fire the event
public class PlayerController : BaseController, IDamageable
{
    public void TakeDamage(DamageBlock damage)
    {
        GameEvents.OnPlayerDamaged?.Invoke(damage);
    }
}

// Subscriber: Listen to the event
public class DamageUIDisplay : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnPlayerDamaged += ShowDamageNumber;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDamaged -= ShowDamageNumber;
    }

    private void ShowDamageNumber(DamageBlock damage)
    {
        // Display damage on screen
    }
}
```

### Benefits
- Loose coupling between systems
- Multiple listeners for one event
- Easy to add new listeners

### When to Use
- UI updates based on game events
- Multiple systems need to react to same event
- Avoiding circular dependencies

---

## 5. State Machine Pattern

### Purpose
Manage object behavior that changes based on its state.

```csharp
public abstract class AIState
{
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}

public class PatrolState : AIState
{
    public override void Update()
    {
        // Patrol behavior
    }
}

public class ChaseState : AIState
{
    public override void Update()
    {
        // Chase player
    }
}

public class EnemyAI : MonoBehaviour
{
    private AIState _currentState;

    public void ChangeState(AIState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
```

### When to Use
- AI behavior management
- Character animation states
- Combat states (combos, charge attacks)
- Menu/UI state management

---

## 6. Strategy Pattern

### Purpose
Define a family of algorithms, encapsulate each, and make them interchangeable.

```csharp
public interface IDamageCalculator
{
    float Calculate(Character attacker, Character defender);
}

public class PhysicalDamageStrategy : IDamageCalculator
{
    public float Calculate(Character attacker, Character defender)
    {
        return attacker.Stats[Stat.AttackPower].FinalValue - defender.Stats[Stat.Defence].FinalValue;
    }
}

public class MagicDamageStrategy : IDamageCalculator
{
    public float Calculate(Character attacker, Character defender)
    {
        return attacker.Stats[Stat.SpellPower].FinalValue - defender.Stats[Stat.MagicResist].FinalValue;
    }
}

public class DamageCalculationSystem
{
    private IDamageCalculator _strategy;

    public void SetStrategy(IDamageCalculator strategy)
    {
        _strategy = strategy;
    }

    public float CalculateDamage(Character attacker, Character defender)
    {
        return _strategy.Calculate(attacker, defender);
    }
}
```

### When to Use
- Different damage calculation methods
- Multiple AI behavior options
- Animation playing strategies
- Movement algorithms

---

## 7. Object Pool Pattern

### Purpose
Improve performance by reusing objects instead of creating/destroying them.

```csharp
public class AudioSourcePool : MonoBehaviour
{
    [SerializeField] private int _poolSize = 10;
    private Queue<AudioSource> _availableSources = new Queue<AudioSource>();
    private List<AudioSource> _usedSources = new List<AudioSource>();

    private void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            _availableSources.Enqueue(source);
        }
    }

    public AudioSource GetAudioSource()
    {
        if (_availableSources.Count > 0)
        {
            AudioSource source = _availableSources.Dequeue();
            source.gameObject.SetActive(true);
            _usedSources.Add(source);
            return source;
        }

        // Create new if pool is empty
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        _usedSources.Add(newSource);
        return newSource;
    }

    public void ReturnAudioSource(AudioSource source)
    {
        source.gameObject.SetActive(false);
        _usedSources.Remove(source);
        _availableSources.Enqueue(source);
    }
}
```

### Implementation in Project
- **ObjectPooling/** folder - Main pooling system
- **Audio/Sources/** - Audio source pooling

### When to Use
- Frequently created and destroyed objects
- Audio sources
- Projectiles
- Particles
- UI elements

---

## 8. Decorator Pattern

### Purpose
Attach additional responsibilities to an object dynamically.

```csharp
public abstract class CharacterDecorator : Character
{
    protected Character _wrappedCharacter;

    public CharacterDecorator(Character character)
    {
        _wrappedCharacter = character;
    }

    public override float GetAttackPower()
    {
        return _wrappedCharacter.GetAttackPower();
    }
}

public class BuffedCharacter : CharacterDecorator
{
    private float _buffMultiplier;

    public BuffedCharacter(Character character, float buffMultiplier) : base(character)
    {
        _buffMultiplier = buffMultiplier;
    }

    public override float GetAttackPower()
    {
        return base.GetAttackPower() * _buffMultiplier;
    }
}

// Usage
var player = new PlayerCharacter();
var buffedPlayer = new BuffedCharacter(player, 1.5f);
var damage = buffedPlayer.GetAttackPower();  // Includes buff
```

### When to Use
- Status effects (buffs/debuffs)
- Equipment bonuses
- Ability modifiers

---

## 9. Template Method Pattern

### Purpose
Define skeleton of algorithm in base class, let subclasses fill in details.

```csharp
public abstract class BaseAbility : MonoBehaviour
{
    public void Execute()
    {
        if (!CanExecute()) return;

        Prepare();
        Cast();
        Apply();
        Finish();
    }

    protected abstract bool CanExecute();
    protected abstract void Prepare();
    protected abstract void Cast();
    protected abstract void Apply();
    protected virtual void Finish() { }
}

public class FireballAbility : BaseAbility
{
    protected override bool CanExecute()
    {
        return player.Mana >= manaCost;
    }

    protected override void Prepare() { }
    protected override void Cast() { CreateFireball(); }
    protected override void Apply() { DealAOEDamage(); }
}
```

### When to Use
- Ability execution system
- Combat sequences
- UI panel initialization
- Character initialization

---

## 10. Adapter Pattern

### Purpose
Convert interface of a class into another interface clients expect.

```csharp
// Legacy class with different interface
public class LegacyAudioPlayer
{
    public void PlayAudio(string filename) { }
}

// New interface we want
public interface IAudioPlayer
{
    void Play(AudioClip clip);
}

// Adapter that makes them compatible
public class AudioPlayerAdapter : IAudioPlayer
{
    private LegacyAudioPlayer _legacyPlayer;

    public AudioPlayerAdapter(LegacyAudioPlayer legacyPlayer)
    {
        _legacyPlayer = legacyPlayer;
    }

    public void Play(AudioClip clip)
    {
        _legacyPlayer.PlayAudio(clip.name);
    }
}
```

### When to Use
- Integrating third-party libraries
- Refactoring legacy code
- Supporting multiple versions

---

## 11. Composite Pattern

### Purpose
Compose objects into tree structures to represent part-whole hierarchies.

```csharp
public abstract class UIElement
{
    public abstract void Render();
}

public class Button : UIElement
{
    public override void Render() { /* Render button */ }
}

public class Panel : UIElement
{
    private List<UIElement> _children = new List<UIElement>();

    public void AddChild(UIElement element)
    {
        _children.Add(element);
    }

    public override void Render()
    {
        // Render panel
        foreach (var child in _children)
        {
            child.Render();
        }
    }
}
```

### When to Use
- UI hierarchies
- GameObject hierarchies
- Menu structures

---

## Pattern Selection Guide

| Pattern | Use When | Example |
|---------|----------|---------|
| Singleton | Need single global instance | AudioManager, GameManager |
| Factory | Complex object creation | ItemFactory, EnemyFactory |
| Observer | Multiple objects react to event | GameEvents system |
| State Machine | Object has distinct states | Enemy AI, Character states |
| Strategy | Multiple algorithm options | Damage calculation |
| Object Pool | Frequent create/destroy | Audio sources, projectiles |
| Decorator | Add features dynamically | Buffs, equipment effects |
| Template Method | Skeleton in base, details in subclasses | Ability system |
| Adapter | Make incompatible interfaces work | Legacy code integration |
| Service Locator | Access managers without dependencies | Get AudioManager anywhere |

---

**Last Updated:** May 15, 2026  
**Version:** 1.0
