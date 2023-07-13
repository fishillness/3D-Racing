using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public class RaceStateTracker : MonoBehaviour
    {
        #region Properties
        public event UnityAction OnPreparationStarted;
        public event UnityAction OnStarted;
        public event UnityAction OnCompleted;
        public event UnityAction<TrackPoint> OnTrackPointPassed;
        public event UnityAction<int> OnCompletedLap;

        [SerializeField] private TrackPointCircuit trackPointCircuit;
        [SerializeField] private int lapsToComplete;

        private RaceState state;
        public RaceState RaceState => state;
        #endregion

        #region Unity Events
        private void Start()
        {
            StartState(RaceState.Preparation);

            trackPointCircuit.OnTrackPointTriggered += OnTrackPointTriggeted;
            trackPointCircuit.OnCompletedLap += OnLapCompleted;
        }

        private void OnDestroy()
        {
            trackPointCircuit.OnTrackPointTriggered -= OnTrackPointTriggeted;
            trackPointCircuit.OnCompletedLap -= OnLapCompleted;
        }
        #endregion

        #region Methods called on an event
        private void OnTrackPointTriggeted(TrackPoint trackPoint)
        {
            OnTrackPointPassed?.Invoke(trackPoint);
        }

        private void OnLapCompleted(int lapAmount)
        {
            if (trackPointCircuit.TrackType == TrackType.Sprint)
            {
                CompleteRace();
            }

            if (trackPointCircuit.TrackType == TrackType.Circular)
            {
                if (lapAmount == lapsToComplete)
                    CompleteRace();
                else
                    CompleteLap(lapAmount);
            }
        }
        #endregion

        #region Race state control
        public void LaunchPreparationStart()
        {
            if (state != RaceState.Preparation) return;

            StartState(RaceState.CountDown);
            OnPreparationStarted?.Invoke();
        }

        private void StartRace()
        {
            if (state != RaceState.CountDown) return;

            StartState(RaceState.Race);
            OnStarted?.Invoke();
        }

        private void CompleteRace()
        {
            if (state != RaceState.Race) return;

            StartState(RaceState.Passed);
            OnCompleted?.Invoke();
        }

        private void CompleteLap(int lapAmount)
        {
            OnCompletedLap?.Invoke(lapAmount);
        }
        #endregion

        private void StartState(RaceState state)
        {
            this.state = state;
        }
    }
}
