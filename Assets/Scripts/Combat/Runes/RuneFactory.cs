using UnityEngine;

namespace Tqa.DungeonQuest.AbilitySystem
{
    [CreateAssetMenu(menuName = "Item/RuneFatory")]
    public class RuneFactory : BaseItemFactory
    {
        [SerializeField]
        private Rune rune;

        public override IItem CreateItem()
        {
            return rune;
        }
    }
}
