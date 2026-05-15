using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Pool;

namespace Tqa.DungeonQuest.AbilitySystem
{
    [CreateAssetMenu(fileName = "Passive rune", menuName = "Rune/Passive")]
    public class PassiveRune : Rune
    {
        [SerializeField]
        private List<BaseEffectFactory> effectsApplyWhenEquip;

        public IEnumerable<BaseEffectFactory> EffectsApplyWhenEquip => effectsApplyWhenEquip;

        public override IItem CreateItem()
        {
            return new PassiveAbility(this);
        }

        private static readonly string[] _types = new string[] { "Rune", "Passive" };

        public override IEnumerable<string> GetSubTypes() => _types;
    }
}
