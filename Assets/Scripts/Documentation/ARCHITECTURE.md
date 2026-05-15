# 2D RPG Project Architecture

## Project Overview
This is a professional 2D RPG game built with Unity, featuring a comprehensive system architecture designed for scalability, maintainability, and team collaboration.

---

## 🎯 Core Principles

1. **Single Responsibility** - Each class has one reason to change
2. **Loose Coupling** - Systems communicate through interfaces and events
3. **High Cohesion** - Related functionality is grouped together
4. **Dependency Injection** - Objects receive dependencies rather than creating them
5. **Testability** - Code is structured to be easily unit tested

---

## 📁 Project Structure Overview

```
Scripts/
├── Core/                  # Global game systems and utilities
├── Gameplay/              # Game feature systems
├── AI/                    # Artificial intelligence systems
├── Input/                 # Input handling and processing
├── Audio/                 # Audio management systems
├── UI/                    # User interface systems
├── ObjectPooling/         # Object pool management
├── Editor/                # Editor-only tools (runs only in editor)
├── Tests/                 # Unit and integration tests
├── Config/                # Game configuration assets
├── Extension/             # Utility extensions (legacy)
├── Demo/                  # Demo scripts
└── Documentation/         # This documentation
```

---

## 🔧 Core Systems

### Core/Definitions/
Centralized enum definitions used throughout the project.

**Purpose:** Single source of truth for all enums  
**Benefits:** No duplicate enums, easy to maintain, global consistency

**Contents:**
- `CombatEnums.cs` - DamageState, DamageType, FighterTeam
- `AbilityEnums.cs` - EffectType, Respond, TargetType, BonusType
- `ItemEnums.cs` - ItemRarity, ItemType
- `StatEnums.cs` - Stat, StatusEffect
- `AudioEnums.cs` - AudioMixerGroup, AudioType

### Core/Data/
Pure data structures without game logic.

**Purpose:** Serializable data containers  
**Used By:** Combat system, stat system, resource management

**Contents:**
- `ResourceBlock.cs` - Health/Mana resource system
- `StatModifier.cs` - Stat modification data

### Core/ScriptableObjects/
All ScriptableObject classes for creating inspector-editable assets.

**Purpose:** Data-driven game design  
**Benefits:** Non-programmers can create and balance game content

**Contents:**
- `BaseStatData.cs` - Character stat configuration
- `ColorData.cs` - Game color palette
- `AudioAsset.cs` - Audio clip asset definition
- Factory classes for item and effect creation

### Core/Interfaces/
Common contracts that multiple systems implement.

**Purpose:** Define system contracts  
**Benefits:** Loose coupling, easy testing, clear requirements

**Key Interfaces:**
- `IDamageable` - Anything that can take damage
- `IDamageDealer` - Anything that deals damage
- `IInteractable` - Interactive objects
- `IPoolable` - Objects in object pool
- `IService` - Game services/managers

### Core/ServiceLocator/
Global dependency injection container.

**Purpose:** Access managers from anywhere without direct references  
**Usage:** `var audioManager = ServiceLocator.Get<AudioManager>()`

### Core/BaseClasses/
Base classes for common functionality.

**Benefits:** Reduces code duplication, provides common patterns

**Classes:**
- `MonoBehaviourSingleton<T>` - Singleton MonoBehaviours
- `Singleton<T>` - Singleton non-MonoBehaviours
- `BaseController` - Base for game controllers
- `BaseManager` - Base for manager systems

### Core/Constants/
Game-wide constant values.

**Purpose:** Prevent magic strings/numbers, centralize configuration

**Contents:**
- `GameConstants.cs` - General game settings
- `LayerConstants.cs` - Layer names (safer than strings)
- `TagConstants.cs` - Tag names (safer than strings)
- `AnimationConstants.cs` - Animation parameter hashes (optimized)

---

## 🎮 Gameplay Systems

### Character/
All character-related systems (player, enemies, bosses).

**Structure:**
```
Character/
├── Player/               - Player-specific systems
│   ├── Movement/         - Player movement input handling
│   ├── Combat/           - Player-specific combat
│   └── Progression/      - Player leveling, skills
├── Generic/              - Reusable base classes
│   ├── Movement/         - Base movement system
│   └── Combat/           - Base combat system
├── Stats/                - Character stat system
│   └── StatusEffects/    - Buffs/debuffs
├── Animation/            - Shared animation logic
└── Boss/                 - Boss-specific systems
    └── Abilities/        - Boss special abilities
```

**Key Classes:**
- `CharacterController` - Base for all characters
- `PlayerController` - Player-specific logic
- `Fighter` - Handles combat for any character
- `CharacterStats` - Character stat system
- `StatusEffectManager` - Buff/debuff system

### Enemy/
All enemy systems including bosses.

**Structure:**
```
Enemy/
├── Base/                 - Base enemy classes
├── Common/               - Generic enemies
│   ├── Melee/            - Melee enemies
│   └── Ranged/           - Ranged enemies
├── Boss/                 - Boss-specific
└── Behaviors/            - Reusable AI behaviors
```

### Items/
Item system (equipment, consumables, abilities, runes).

**Structure:**
```
Items/
├── Core/                 - Base item classes
├── Equipment/            - Equipment system
├── Consumables/          - Potions, etc
├── Abilities/            - Ability items
├── Runes/                - Rune items
├── Factory/              - Item creation
└── Manager/              - Inventory, slots
```

### Interaction/
Interactive objects and systems.

**Structure:**
```
Interaction/
├── Core/                 - Base interactable classes
├── Interactables/        - Doors, chests, portals, NPCs
├── Traps/                - Hazards and traps
├── Indicators/           - Visual feedback
└── Effects/              - Interaction effects
```

### Progression/
Level progression, spawning, difficulty.

**Contents:**
- Level manager system
- Enemy spawning system
- Difficulty scaling
- Stage management

---

## 🤖 AI Systems

### AI/Pathfinding/
A* pathfinding implementation.

**Usage:** For NPC and enemy movement

### AI/Behavior/
AI behavior trees and decision making.

**Structure:**
- Core behavior classes
- Movement behaviors (patrol, chase, flee)
- Combat behaviors (attack, dodge, buff)
- Perception system (sight, hearing, smell)

---

## 🎨 UI Systems

Comprehensive, well-organized UI system.

**Structure:**
```
UI/
├── Core/                 - Base UI classes
├── HUD/                  - Always-visible elements
├── Combat/               - Combat feedback (casting bar, damage)
├── Panels/               - Modal panels
│   ├── Base/             - Panel base classes
│   ├── Inventory/        - Inventory and items
│   ├── Character/        - Character stats and abilities
│   ├── Confirmation/     - Confirmation dialogs
│   └── Shop/             - Shop system
├── WorldUI/              - World-space UI (health bars, labels)
├── Buttons/              - Button interaction handlers
├── Effects/              - UI animations and effects
├── Transitions/          - Scene/panel transitions
├── Layout/               - Reusable layouts
└── Themes/               - UI theming system
```

---

## 🔊 Audio Systems

Organized audio management.

**Structure:**
```
Audio/
├── Manager/              - Central audio manager
├── Sources/              - Audio source pooling
├── Clips/                - Sound libraries
└── Utilities/            - Audio helpers
```

**Features:**
- Audio pooling for performance
- Mixer group management
- Music and SFX separation
- Audio caching

---

## 🏗️ Architecture Patterns Used

### 1. **Singleton Pattern**
- `MonoBehaviourSingleton<T>` for managers
- `Singleton<T>` for non-MonoBehaviour singletons

### 2. **Service Locator Pattern**
- Global access to managers through ServiceLocator
- Reduces hard dependencies

### 3. **STATE MACHINE Pattern**
- AI state management
- UI panel states
- Combat states

### 4. **FACTORY Pattern**
- Item creation
- Enemy creation
- Effect creation

### 5. **OBSERVER Pattern**
- Event system for loose coupling
- UI updates triggered by events
- AI perception system

### 6. **STRATEGY Pattern**
- Different AI behaviors
- Different animation strategies
- Different damage calculations

### 7. **OBJECT POOL Pattern**
- Audio source pooling
- Projectile pooling
- Enemy pooling

---

## 📝 Naming Conventions

### Classes
- `Manager` - Manages systems (AudioManager, UIManager)
- `Controller` - Controls entities (PlayerController, EnemyController)  
- `System` - Core systems (ParticleSystem, SaveSystem)
- `Handler` - Event/callback handlers (EventHandler, InputHandler)
- `Factory` - Creates objects (ItemFactory, EnemyFactory)
- `Base` - Abstract base classes (BaseController, BaseManager)

### Interfaces
- `I` prefix (IService, IDamageable, IInteractable)

### Files
- One public class per file (exceptions: related enums)
- Filename matches public class name
- Use PascalCase for all identifiers

### Folders
- PascalCase for all folder names
- Feature-based organization
- Logical grouping of related systems

---

## 🚀 Best Practices

### 1. Always Use Interfaces
```csharp
public class EnemyController : BaseController, IDamageable
{
    // All that can take damage implement IDamageable
}
```

### 2. Use Constants, Not Magic Strings
```csharp
// Bad
if(gameObject.layer == 8) { }

// Good
if(gameObject.layer == LayerConstants.ENEMY) { }
```

### 3. Use ServiceLocator for Managers
```csharp
// Instead of: FindObjectOfType<AudioManager>()
var audioManager = ServiceLocator.Get<AudioManager>();
```

### 4. Use Events for Communication
```csharp
// Fire event instead of direct method calls
GameEvents.OnPlayerDamaged?.Invoke(damage);
```

### 5. Keep Classes Small and Focused
- One responsibility per class
- Consider breaking into smaller classes if > 200 lines

### 6. Use Base Classes
```csharp
// Reuse base functionality
public class PlayerController : BaseController
{
    // Player-specific logic only
}
```

---

## 📚 Contributing Guidelines

### Adding a New Feature

1. **Determine Category** - Where does it logically belong?
   - Character system? → Character/
   - Enemy AI behavior? → AI/Behavior/
   - UI panel? → UI/Panels/

2. **Create Base Class**
   - Inherit from appropriate base class (BaseController, BaseManager, etc.)
   - Implement appropriate interfaces

3. **Follow Naming Conventions**
   - Class names match file names
   - Use consistent prefixes/suffixes

4. **Add Documentation**
   - Add XML comments to public methods
   - Add region comments for organization

5. **Use ServiceLocator**
   - Don't create hard dependencies
   - Register services with ServiceLocator

6. **Test Your Code**
   - Create unit tests in Tests/Unit/
   - Test different scenarios

---

## 🔍 Code Organization Checklist

Before committing, verify:
- ✅ Classes in correct folder
- ✅ Following naming conventions
- ✅ Using correct base classes/interfaces
- ✅ No hard dependencies (use ServiceLocator or events)
- ✅ Magic values replaced with constants
- ✅ Code is DRY (Don't Repeat Yourself)
- ✅ Comments explain "why", not "what"
- ✅ No unused using statements

---

## 🔗 See Also

- `CODE_STYLE_GUIDE.md` - Detailed code style guidelines
- `DESIGN_PATTERNS.md` - Pattern implementation examples
- Individual system documentation in their folders

---

**Last Updated:** May 15, 2026  
**Version:** 2.0 - Professional Restructuring
