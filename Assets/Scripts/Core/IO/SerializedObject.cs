using System;

namespace Tqa.DungeonQuest.IOSystem
{
    [Serializable]
    public abstract class SerializedObject
    {
        public abstract SerializedType GetSerializedType();
        public abstract object Deserialize();
    }
}
