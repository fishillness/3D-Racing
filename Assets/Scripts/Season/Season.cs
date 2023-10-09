using UnityEngine;

namespace Racing
{
    [CreateAssetMenu]
    public class Season : ScriptableObject
    {
        [SerializeField] private string seasonName;
        [SerializeField] private RaceInfo[] raceInfos;
        [SerializeField] private Sprite icon;

        public string SeasonName => seasonName;
        public RaceInfo[] RaceInfos => raceInfos;
        public Sprite Icon => icon;
    }
}
