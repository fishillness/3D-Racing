using UnityEngine;

namespace Racing
{
    public class RaceInputController : MonoBehaviour
    {
        [SerializeField] private CarInputControl carControl;
        [SerializeField] private RaceStateTracker raceStateTracker;

        private void Start()
        {
            carControl.enabled = false;

            raceStateTracker.OnStarted += OnRaceStarted;
            raceStateTracker.OnCompleted += OnRaceFinished;
        }

        private void OnDestroy()
        {
            raceStateTracker.OnStarted -= OnRaceStarted;
            raceStateTracker.OnCompleted -= OnRaceFinished;
        }

        private void OnRaceStarted()
        {
            carControl.enabled = true;
        }

        private void OnRaceFinished()
        {
            carControl.Stop();
            carControl.enabled = false;
        }
    }
}
