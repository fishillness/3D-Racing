using UnityEngine;

namespace Racing
{
    public class SceneDependencies : MonoBehaviour
    {
        [SerializeField] private TrackPointCircuit trackPointCircuit;

        private void Awake()
        {
            MonoBehaviour[] allMono = FindObjectsOfType<MonoBehaviour>();
            
            for (int i = 0; i < allMono.Length; i++)
            {
                if (allMono[i] is IDependencyTrackPointCircuit)
                {
                    (allMono[i] as IDependencyTrackPointCircuit).Construct(trackPointCircuit);
                }
            }
        }

    }
}
