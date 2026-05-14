using System;
using UnityEngine;

namespace Tqa.DungeonQuest.ObjectPooling
{
    public interface IPoolObject
    {
        GameObject gameObject { get; }
        void Init(Action<IPoolObject> returnAction);

        void ReturnToPool();
    }
}
