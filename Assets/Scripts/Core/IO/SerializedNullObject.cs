using System;

namespace Tqa.DungeonQuest.IOSystem
{
    [Serializable]
    public class SerializedNullObject : SerializedObject
    {
        public override object Deserialize()
        {
            return null;
        }

        public override SerializedType GetSerializedType() => SerializedType.Null;

        public static readonly SerializedNullObject intance = new();
    }
}
