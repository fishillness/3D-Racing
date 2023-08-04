using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UITrackTime : MonoBehaviour,
        IDependency<RaceTimeTracker>, IDependency<RaceStateTracker>
    {
        [SerializeField] private Text text;


        private RaceTimeTracker raceTimeTracker;
        public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;

        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        private void Start()
        {
            raceStateTracker.OnStarted += OnRaceStarted;
            raceStateTracker.OnCompleted += OnRaceCompleted;

            text.enabled = false;
        }

        private void Update()
        {
            text.text = StringTime.SecondToTimeString(raceTimeTracker.CurrentTime);
        }

        private void OnDestroy()
        {
            raceStateTracker.OnStarted -= OnRaceStarted;
            raceStateTracker.OnCompleted -= OnRaceCompleted;
        }

        private void OnRaceStarted()
        {
            text.enabled = true;
            enabled = true;
        }

        private void OnRaceCompleted()
        {
            text.enabled = false;
            enabled = false;
        }

    }
}
