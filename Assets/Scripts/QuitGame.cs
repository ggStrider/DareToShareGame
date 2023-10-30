using UnityEngine;

namespace qs.Settings
{
    public class QuitGame : MonoBehaviour
    {
        public void ExitGame() =>
            Application.Quit();
    }
}