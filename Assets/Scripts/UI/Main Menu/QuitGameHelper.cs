using UnityEngine;

namespace Tqa.DungeonQuest.MainMenu
{
    public class QuitGameHelper : MonoBehaviour
    {
        public void QuitGame()
        {
            Game.Quit();
        }
    }
}
