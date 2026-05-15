using System.Collections.Generic;
using Qanht.NightRealm.AbilitySystem;
using Qanht.NightRealm.AbilitySystem.Spell;
using Qanht.NightRealm.ObjectPooling;
using UnityEngine;

namespace Qanht.NightRealm.TheBoss
{
    public class BossDamageZoneSpellCenter : PoolObject, ISpell
    {
        [SerializeField]
        private Prefab damageZoneSpellPrefab;

        [SerializeField]
        private List<Transform> damageZoneReleasePositions;

        public void KickOff(OrientationAbility ability, Vector2 direction)
        {
            transform.position = ability.Caster.Owner.Position;
            foreach (Transform transform in damageZoneReleasePositions)
            {
                if (PoolManager.Get<ISpell>(damageZoneSpellPrefab, out var spell))
                {
                    spell.KickOff(ability, transform.position);
                }
            }
            ReturnToPool();
        }
    }
}
