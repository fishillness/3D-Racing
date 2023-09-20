using UnityEngine;

namespace Racing
{
    [CreateAssetMenu]
    public class RaceInfo : ScriptableObject
    {
        [SerializeField] private string sceneName;
        [SerializeField] private Sprite icon;
        [SerializeField] private string title;

        [SerializeField] private float goldTime;
        [SerializeField] private float silverTime;
        [SerializeField] private float bronzeTime;

        public string SceneName => sceneName;
        public Sprite Icon => icon;
        public string Title => title;

        public float GoldTime => goldTime;
        public float SilverTime => silverTime;
        public float BronzeTime => bronzeTime;
    }
}

