using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public class RaceStateTracker : MonoBehaviour, IDependencyTrackPointCircuit
    {
        #region Properties
        public event UnityAction OnPreparationStarted;
        public event UnityAction OnStarted;
        public event UnityAction OnCompleted;
        public event UnityAction<TrackPoint> OnTrackPointPassed;
        public event UnityAction<int> OnCompletedLap;

        [SerializeField] private Timer countdownTimer;
        [SerializeField] private int lapsToComplete;

        private TrackPointCircuit trackpointCircuit;
        public void Construct(TrackPointCircuit trackpointCircuit) =>
                           this.trackpointCircuit = trackpointCircuit;

        private RaceState state;
        public RaceState RaceState => state;
        #endregion

        #region Unity Events
        private void Start()
        {
            StartState(RaceState.Preparation);
            countdownTimer.enabled = false;

            trackpointCircuit.OnTrackPointTriggered += OnTrackPointTriggeted;
            trackpointCircuit.OnCompletedLap += OnLapCompleted;
            countdownTimer.OnFinished += OnCountdownTimerFinished;
        }

        private void OnDestroy()
        {
            trackpointCircuit.OnTrackPointTriggered -= OnTrackPointTriggeted;
            trackpointCircuit.OnCompletedLap -= OnLapCompleted;
            countdownTimer.OnFinished -= OnCountdownTimerFinished;
        }
        #endregion


        #region Methods called on an event
        private void OnTrackPointTriggeted(TrackPoint trackPoint)
        {
            OnTrackPointPassed?.Invoke(trackPoint);
        }

        private void OnLapCompleted(int lapAmount)
        {
            if (trackpointCircuit.TrackType == TrackType.Sprint)
            {
                CompleteRace();
            }

            if (trackpointCircuit.TrackType == TrackType.Circular)
            {
                if (lapAmount == lapsToComplete)
                    CompleteRace();
                else
                    CompleteLap(lapAmount);
            }
        }

        private void OnCountdownTimerFinished()
        {
            StartRace();
        }
        #endregion

        #region Race state control
        public void LaunchPreparationStart()
        {
            if (state != RaceState.Preparation) return;

            StartState(RaceState.CountDown);
            countdownTimer.enabled = true;
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
