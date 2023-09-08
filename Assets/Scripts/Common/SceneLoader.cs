using UnityEngine;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class SceneLoader : MonoBehaviour
    {
        public const string MainMenuSceneTitle = "Main_menu";
        private SeasonList seasons;
        private LevelIndex levelIndex = null;

        public SeasonList Seasons => seasons;
        public bool IsLastLevel => !(levelIndex.number < levelIndex.season.RaceInfos.Length - 1);

        private void Start()
        {
            seasons = GameObject.FindObjectOfType<SeasonList>();
            levelIndex = LevelUtil.DetermineSeasonAndLevelIndex(seasons, SceneManager.GetActiveScene().name);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(MainMenuSceneTitle);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadNexLevel()
        {
            if (!IsLastLevel)
            {
                SceneManager.LoadScene(levelIndex.season.RaceInfos[levelIndex.number + 1].SceneName);
            }
            else
                Debug.Log("This is a last level");
        }


        public void OnApplicationQuit()
        {
            Application.Quit();
        }

    }
}
