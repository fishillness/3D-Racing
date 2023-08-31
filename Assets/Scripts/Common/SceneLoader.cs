using UnityEngine;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class SceneLoader : MonoBehaviour
    {
        public const string MainMenuSceneTitle = "Main_menu";

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(MainMenuSceneTitle);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
