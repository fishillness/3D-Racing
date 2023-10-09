using UnityEngine;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class LevelDefiner : MonoBehaviour
    {
        private LevelIndex levelIndex;
        private string sceneName;

        public string SceneName => sceneName;
        public int LevelNumber => levelIndex.number;
        public Season LevelSeason => levelIndex.season;
        public RaceInfo RaceInfo => levelIndex.season.RaceInfos[levelIndex.number];

        private void Awake()
        {
            sceneName = SceneManager.GetActiveScene().name;
            SeasonList seasonList = GameObject.FindObjectOfType<SeasonList>();
            levelIndex = LevelUtil.DetermineSeasonAndLevelIndex(seasonList, sceneName);
        }
    }
}
