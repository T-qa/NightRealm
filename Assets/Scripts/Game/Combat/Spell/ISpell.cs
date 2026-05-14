using Tqa.DungeonQuest.ObjectPooling;
using UnityEngine;

namespace Tqa.DungeonQuest.AbilitySystem.Spell
{
    public interface ISpell : IPoolObject
    {
        void KickOff(OrientationAbility ability, Vector2 direction);
    }
}
