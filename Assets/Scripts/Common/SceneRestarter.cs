using UnityEngine;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class SceneRestarter : MonoBehaviour
    {
        //DEBUG
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace) == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (Input.GetKeyDown(KeyCode.F5) == true)
            {
                Saves.DeleteAllSaves();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
