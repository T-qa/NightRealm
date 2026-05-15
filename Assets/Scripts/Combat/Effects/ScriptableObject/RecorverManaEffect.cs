using UnityEngine;

namespace Qanht.NightRealm.AbilitySystem
{
    [CreateAssetMenu(fileName = "Recovery Effect", menuName = "Effects/Instant/RecorverMana")]
    public class RecorverManaEffect : BaseEffectAndFactory
    {
        [SerializeField]
        protected StatBasedValue basedValue;

        public override void Instanciate(Fighter source, Fighter receiver)
        {
            float healAmount = basedValue.GetRawValue(source);
            receiver.Mana.Recover(healAmount);
        }
    }
}
