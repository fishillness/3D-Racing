
namespace Racing
{
    public static class LevelUtil
    {
        public static LevelIndex DetermineSeasonAndLevelIndex(SeasonList seasonList, string levelName)
        {
            for (int i = 0; i < seasonList.Seasons.Length; i++)
            {
                for (int j = 0; j < seasonList.Seasons[i].RaceInfos.Length; j++) //LevelsName.Length; j++)
                {
                    if (seasonList.Seasons[i].RaceInfos[j].SceneName == levelName) //LevelsName[j] == levelName)
                    {
                        return new LevelIndex { season = seasonList.Seasons[i], number = j };
                    }
                }
            }

            return null;
        }
    }
}
