using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UIControlTips : MonoBehaviour,
        IDependency<RaceStateTracker>, IDependency<LevelDefiner>
    {
        [SerializeField] private GameObject startTip;
        [SerializeField] private Text sceneNameText; 

        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        private LevelDefiner levelDefiner;
        public void Construct(LevelDefiner obj) => levelDefiner = obj;

        private void Start()
        {
            sceneNameText.text = levelDefiner.SceneName;
            sceneNameText.gameObject.SetActive(true);
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
            sceneNameText.gameObject.SetActive(false);
        }
    }
}
