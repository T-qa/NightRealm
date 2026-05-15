using UnityEngine;

namespace Tqa.DungeonQuest.ObjectPooling
{
    [RequireComponent(typeof(IPoolObject))]
    public class Prefab : MonoBehaviour
    {
        [field: SerializeField]
        public string UniquePrefabID { get; private set; }
    }
}
