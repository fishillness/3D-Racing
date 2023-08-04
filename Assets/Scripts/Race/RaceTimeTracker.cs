using UnityEngine;

namespace Racing
{
    public class RaceTimeTracker : MonoBehaviour, IDependency<RaceStateTracker>
    {    
        private float currentTime;
        public float CurrentTime => currentTime;

        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        private void Start()
        {
            raceStateTracker.OnStarted += OnRaceStarted;
            raceStateTracker.OnCompleted += OnRaceCompleted;            

            enabled = false;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
        }

        private void OnDestroy()
        {
            raceStateTracker.OnStarted -= OnRaceStarted;
            raceStateTracker.OnCompleted -= OnRaceCompleted;
        }

        private void OnRaceStarted()
        {
            enabled = true;
            currentTime = 0;
        }

        private void OnRaceCompleted()
        {
            enabled = false;
        }


    }
}

