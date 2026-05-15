using TMPro;
using Qanht.NightRealm.ObjectPooling;
using UnityEngine;

namespace Qanht.NightRealm.Communicate
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
