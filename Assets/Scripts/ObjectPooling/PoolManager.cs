using System.Collections.Generic;
using Tqa.DungeonQuest.EventManagers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tqa.DungeonQuest.ObjectPooling
{
    public static class PoolManager
    {
        private static readonly Dictionary<string, ObjectPool> _pools;

        static PoolManager()
        {
            _pools = new();
            SceneManager.sceneUnloaded += ClearPool;
            EventManager.AddListener("OnMapChanging", EmptyPool);
            EventManager.AddListener("OnMapChanged", EmptyPool);
        }

        public static bool Get<T>(Prefab prefab, out T instance)
            where T : IPoolObject
        {
            try
            {
                if (!_pools.TryGetValue(prefab.UniquePrefabID, out var pool))
                {
                    pool = new ObjectPool(prefab.gameObject);
                    _pools[prefab.UniquePrefabID] = pool;
                }
                instance = (T)pool.GetFromPool();
                return true;
            }
            catch
            {
                string info =
                    prefab == null ? "Null prefab" : $"prefab with id : {prefab.UniquePrefabID}";
                Debug.LogError($"Error when try to get {typeof(T)} from pool by {info}");
                instance = default;
                return false;
            }
        }

        public static void EmptyPool()
        {
            foreach (var pool in _pools.Values)
            {
                pool.ClearPool();
            }
            _pools.Clear();
        }

        private static void ClearPool(Scene _)
        {
            EmptyPool();
        }
    }
}
