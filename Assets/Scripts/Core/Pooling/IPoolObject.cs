using System;
using UnityEngine;

namespace Qanht.NightRealm.ObjectPooling
{
    public interface IPoolObject
    {
        GameObject gameObject { get; }
        void Init(Action<IPoolObject> returnAction);

        void ReturnToPool();
    }
}
