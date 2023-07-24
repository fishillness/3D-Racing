using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UICountdownTimer : MonoBehaviour, IDependency<RaceStateTracker>
    {
        [SerializeField] private Text text;

        private Timer countdownTimer;
        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        private void Start()
        {
            raceStateTracker.OnPreparationStarted += OnPreparationStarted;
            raceStateTracker.OnStarted += OnRaceStarted;

            text.enabled = false;
        }

        private void Update()
        {
            text.text = raceStateTracker.CountDownTimer.Value.ToString("F0");

            if (text.text == "0")
                text.text = "GO!";
        }

        private void OnDestroy()
        {
            raceStateTracker.OnPreparationStarted -= OnPreparationStarted;
            raceStateTracker.OnStarted -= OnRaceStarted;
        }

        private void OnPreparationStarted()
        {
            text.enabled = true;
            enabled = true;
        }

        private void OnRaceStarted()
        {
            text.enabled = false;
            enabled = false;
        }
    }
}
