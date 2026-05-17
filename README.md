# 🎮 Night Realm

A 2D top-down RPG built with **Unity** featuring dynamic combat, strategic dungeon exploration, pathfinding AI enemies, and a rich progression system.

<p align="center">
  <a href="https://qanht.itch.io/night-realm">
    <img src="https://img.shields.io/badge/🎮_Play_on_itch.io-Night_Realm-fa5c5c?style=for-the-badge&logo=itchdotio&logoColor=white" />
  </a>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Status-Active%20Development-brightgreen" />
  <img src="https://img.shields.io/badge/Unity-6000.3.10f1-blue" />
  <img src="https://img.shields.io/badge/License-MIT-green" />
</p>

---

## 🎯 Overview

**Night Realm: Dungeon Quest** is an immersive 2D top-down RPG that combines classic dungeon-crawling gameplay with modern game design. Explore a mysterious town, venture into dangerous dungeons, defeat AI-driven enemies, and battle epic bosses. Customize your character with runes, equipment, and consumables to overcome increasingly challenging encounters.

### Quick Start
- **Game Scenes**: Menu → CutScene → Game (Town) → DungeonFloor1 → BossMap
- **Editor**: Open in Unity 6000.3.10f1 or later
- **Play**: Press `Play` in the editor or build for your platform

---

## 🎬 Gameplay Demo

[![Watch Gameplay](https://img.shields.io/badge/▶️-Watch%20Gameplay%20Video-blue?style=for-the-badge)](Docs/Video%20Gameplay.mp4)

[📽️ Full Gameplay Walkthrough](Docs/Video%20Gameplay.mp4) - Watch the video to see the core gameplay, combat mechanics, and dungeon exploration in action!

---

## ✨ Features

### 🎮 Core Gameplay
- **Hub-and-Dungeon Flow**: Safe town hub with quests, shops, and storage; dangerous dungeons with staged encounters
- **Combat System**: Real-time combat with health, mana, and dynamic damage calculation
- **Character Progression**: 
  - Level system with XP gains from defeated enemies
  - Character stats with equipment-based scaling
  - Rune-based skill system (active, passive, self-cast, targeting)
  - Equipment and consumable items with stat bonuses and effects

### 🤖 AI & Pathfinding
- **Custom A* Pathfinding**: Efficient grid-based pathfinding for enemy movement
- **Smart AI Enemies**:
  - Melee and ranged combat behavior
  - Pathfinding-driven movement and pursuit
  - Boss-specific abilities and patterns
  - Minion AI with support mechanics

### 🏰 World & Content
- **Multiple Maps**: Town hub, multiple dungeon floors, boss arenas
- **NPC Interactions**: Dialogue, quests, and story progression
- **Dynamic Encounters**: Wave-based spawning, stage triggers, environmental challenges
- **Boss Fights**: Unique boss mechanics with custom ability systems

### 🎒 Progression Systems
- **Inventory Management**: Items, equipment, consumables, runes
- **Equipment System**: Armor and weapons with stat modifiers
- **Skill Slots**: Customize active and passive abilities
- **Shop & Trading**: Buy, sell, and trade with NPCs
- **Chest Storage**: Persistent item storage across sessions

### 💾 Persistence
- **Save/Load System**: Auto-save and manual save with JSON serialization
- **Player Data**: Level, stats, inventory, equipment, skill configuration
- **Game State**: Enemy spawns, environmental changes, NPC states

### 🎨 Audio & Visual
- **Dynamic Audio**: Pooled audio sources, sound effects, background music
- **Visual Effects**: Hit feedback, floating damage numbers, VFX for abilities
- **UI/UX**: Intuitive menus, tooltips, health bars, casting bars, stat displays

### ⌨️ Input System
- **Modern Input System**: Unity's new Input System integration
- **Multiple Input Types**: Movement, ability casting, interactions
- **Rebindable Controls**: Support for custom keybindings

---

## 🛠️ Technical Stack

| Component | Technology |
|---|---|
| **Engine** | Unity 6000.3.10f1 |
| **Graphics** | Universal Render Pipeline (URP) + 2D Sprite Suite |
| **Input** | Unity Input System |
| **Animation** | Animator Controller, Timeline |
| **UI** | UGUI (Canvas), TextMesh Pro |
| **Camera** | Cinemachine |
| **Physics** | 2D Physics |
| **Audio** | Audio Mixer, AudioClips |
| **VFX** | Particle System, DOTween |
| **Pathfinding** | Custom A* Implementation |

### Architecture Highlights

```
Assets/
├── Scripts/
│   ├── Pathfinding/        # A* grid-based pathfinding system
│   ├── Charactor/          # Player and enemy controllers
│   │   ├── Player/         # Player input and combat
│   │   ├── Monsters/       # Enemy AI and behavior
│   │   └── Boss/           # Boss-specific mechanics
│   ├── Game/               # Core game systems
│   │   ├── Combat/         # Damage, effects, resources
│   │   ├── GameManager/    # Game state and flow
│   │   └── LevelManager/   # Map and progression
│   ├── UI/                 # UI controllers and panels
│   ├── Item/               # Item system (inventory, equipment)
│   ├── AudioManager/       # Sound and music management
│   ├── Extension/          # Utility extensions
│   └── InputSystem/        # Input handling
├── Prefabs/                # Reusable game objects
├── Scenes/                 # Game scenes (Menu, Town, Dungeons, Boss)
├── Art/                    # Sprites, animations, character assets
├── Resources/              # Runtime-loaded assets (audio, items)
└── Settings/               # URP and project settings
```

---

## 🚀 Getting Started

### Prerequisites
- Unity 6000.3.10f1 or later
- C# 9.0 or later
- Windows/Mac/Linux for development

### Installation
1. Clone or download this repository
2. Open the project in Unity
3. Load the `Menu` scene in build settings
4. Press `Play` in the editor

### Building
```bash
# From Unity Editor:
1. File > Build Settings
2. Select target platform
3. Click "Build"
```

---

## 📖 Documentation

Comprehensive developer documentation is available in:
- **[GAME_DEV_GUIDE.md](./Docs/GAME_DEV_GUIDE.md)** - Complete GDD, architecture, and implementation details

### Key Systems Documentation

| System | Location | Purpose |
|--------|----------|---------|
| Combat | `Assets/Scripts/Game/Combat/` | Damage calculation, effects, resources (health/mana) |
| AI/Pathfinding | `Assets/Scripts/Pathfinding/` & `Assets/Scripts/Charactor/Monsters/` | Enemy movement and behavior |
| Inventory | `Assets/Scripts/Item/` | Item management, equipment, consumables |
| Progression | `Assets/Scripts/Game/LevelManager/` | Experience, leveling, stat scaling |
| UI | `Assets/Scripts/UI/` | User interface and menus |
| Save/Load | `Assets/Scripts/IOSystem/` | Game state persistence |

---

## 🎮 Gameplay Guide

### Controls
- **Movement**: `WASD` or Arrow Keys
- **Ability Cast**: `LMB` (left mouse button) / `Q`,`E`,`R` (ability slots)
- **Interact**: `F` (NPCs, doors, chests)
- **Inventory**: `I`
- **Equipment**: `C`
- **Skills**: `K`

### Core Loop
1. **Town Exploration** - Interact with NPCs, upgrade equipment, purchase items
2. **Dungeon Entry** - Select a dungeon and prepare
3. **Combat** - Defeat waves of enemies using runes and abilities
4. **Boss Encounter** - Face powerful boss enemies with unique mechanics
5. **Rewards** - Earn XP, loot, and progress to the next challenge
6. **Repeat** - Return to town, upgrade, and tackle harder content

### Progression Tips
- **Prioritize Equipment**: Gear directly scales your stats
- **Experiment with Runes**: Different ability combinations suit different playstyles
- **Use Consumables Wisely**: Save powerful potions for boss fights
- **Level Up**: Gain experience from every enemy defeated
- **Storage Management**: Use the town chest to manage your inventory

---

## 🤝 Contributing

This is an active development project. To contribute:

1. Create a feature branch (`git checkout -b feature/YourFeature`)
2. Commit your changes (`git commit -m 'Add YourFeature'`)
3. Push to the branch (`git push origin feature/YourFeature`)
4. Open a Pull Request

### Development Workflow
- Follow the existing code style and architecture
- Test changes in the editor before committing
- Update documentation for new features
- Keep commits atomic and descriptive

---

## 🐛 Roadmap

### Planned Features
- [ ] Additional dungeon floors with unique themes
- [ ] More boss encounters with varied mechanics
- [ ] Expanded rune variety and skill tree system
- [ ] Multiplayer/cooperative gameplay
- [ ] Mobile platform support
- [ ] Advanced visual effects and animations
- [ ] Sound design and music enhancements
- [ ] Difficulty modes (Normal, Hard, Nightmare)

---

## 📊 Project Stats

| Metric | Value |
|--------|-------|
| Total Scripts | 50+ |
| Total Scenes | 6 |
| Supported Platforms | Windows, Mac, Linux, WebGL |
| Target Audience | PC/Mobile gamers |
| Development Status | Active Development |

---

## 📝 License

This project is licensed under the **MIT License** - see the [LICENSE](./LICENSE) file for details.

---

## 📧 Contact & Support

- **Repository**: [T-qa/NightRealm](https://github.com/T-qa/NightRealm)
- **Issues**: Report bugs or request features via GitHub Issues
- **Discussions**: Use GitHub Discussions for questions and ideas

---

## 🙏 Acknowledgments

Thanks to the Unity community and all contributors who have helped shape this project!
