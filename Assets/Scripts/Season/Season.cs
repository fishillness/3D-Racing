using UnityEngine;

namespace Racing
{
    [CreateAssetMenu]
    public class Season : ScriptableObject
    {
        [SerializeField] private string seasonName;
        //[SerializeField] private string[] levelsName;
        [SerializeField] private RaceInfo[] raceInfos;
        [SerializeField] private Sprite icon;

        public string SeasonName => seasonName;
        //public string[] LevelsName => levelsName;
        public RaceInfo[] RaceInfos => raceInfos;
        public Sprite Icon => icon;
    }
}
