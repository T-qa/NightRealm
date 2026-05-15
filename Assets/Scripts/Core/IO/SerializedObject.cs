using System;

namespace Qanht.NightRealm.IOSystem
{
    [Serializable]
    public abstract class SerializedObject
    {
        public abstract SerializedType GetSerializedType();
        public abstract object Deserialize();
    }
}
