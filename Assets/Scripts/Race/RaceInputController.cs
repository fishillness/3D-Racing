using UnityEngine;

namespace Racing
{
    public class RaceInputController : MonoBehaviour,
        IDependency<CarInputControl>, IDependency<RaceStateTracker>
    {
        private CarInputControl carControl;
        public void Construct(CarInputControl obj) => carControl = obj;

        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;


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
