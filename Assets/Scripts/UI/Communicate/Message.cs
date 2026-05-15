using TMPro;
using Tqa.DungeonQuest.ObjectPooling;
using UnityEngine;

namespace Tqa.DungeonQuest.Communicate
{
    public class Message : PoolObject
    {
        [SerializeField]
        private TextMeshProUGUI textUI;

        public void SetMessage(string sender, string message)
        {
            textUI.text = $"{sender} : {message}";
        }

        public void SetSystemMessage(string message)
        {
            textUI.text = $"[System] {message}";
        }
    }
}
