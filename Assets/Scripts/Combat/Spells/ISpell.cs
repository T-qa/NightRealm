using Qanht.NightRealm.ObjectPooling;
using UnityEngine;

namespace Qanht.NightRealm.AbilitySystem.Spell
{
    public interface ISpell : IPoolObject
    {
        void KickOff(OrientationAbility ability, Vector2 direction);
    }
}
