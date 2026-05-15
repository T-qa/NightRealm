using UnityEngine;

namespace Qanht.NightRealm.ObjectPooling
{
    [RequireComponent(typeof(IPoolObject))]
    public class Prefab : MonoBehaviour
    {
        [field: SerializeField]
        public string UniquePrefabID { get; private set; }
    }
}
