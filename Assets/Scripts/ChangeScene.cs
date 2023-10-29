using UnityEngine;
using UnityEngine.SceneManagement;

namespace qs.Scenes
{
    public class ChangeScene : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void OnChangeScene() =>
            SceneManager.LoadScene(_sceneName);
    }
}