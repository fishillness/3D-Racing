using UnityEngine;

namespace Racing
{
    public class UIControlTips : MonoBehaviour, IDependency<RaceStateTracker>
    {
        [SerializeField] private GameObject startTip;

        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        private void Start()
        {
            startTip.SetActive(true);
            raceStateTracker.OnPreparationStarted += TurnOffStartTip;
        }

        private void OnDestroy()
        {
            raceStateTracker.OnPreparationStarted -= TurnOffStartTip;
        }

        private void TurnOffStartTip()
        {
            gameObject.SetActive(false);
        }
    }
}
