using System.Collections.Generic;
using System.Text;
using Qanht.NightRealm.ObjectPooling;
using UnityEngine;

namespace Qanht.NightRealm.AbilitySystem
{
    [CreateAssetMenu(fileName = "Targeting rune", menuName = "Rune/Targeting")]
    public class TargetingRune : ActiveRune
    {
        [field: SerializeField]
        public Prefab VfxPlayWhenActive { get; private set; }

        [SerializeField]
        private List<BaseEffectFactory> effectsApplyToTarget;
        public IEnumerable<BaseEffectFactory> EffectsApplyToTarget => effectsApplyToTarget;

        public override IItem CreateItem()
        {
            return new TargetingAbility(this);
        }

        private static readonly string[] _types = new string[] { "Rune", "Targeting" };

        public override IEnumerable<string> GetSubTypes() => _types;
    }
}
