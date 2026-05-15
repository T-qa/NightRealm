using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tqa.DungeonQuest.IOSystem
{
    public interface ISerializable
    {
        SerializedObject Serialize();
    }
}
