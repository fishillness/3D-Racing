using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UIRaceResultPanel : MonoBehaviour, IDependency<RaceResultTime>
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private Text resultText;
        [SerializeField] private GameObject congratulations;
        [SerializeField] private Text goldTimeText;
        [SerializeField] private Text recordText;

        private RaceResultTime raceResultTime;
        public void Construct(RaceResultTime obj) => raceResultTime = obj;
        
        private void Start()
        {
            raceResultTime.OnResultsUpdated += DisplayResult;
            panel.SetActive(false);
        }

        private void OnDestroy()
        {
            raceResultTime.OnResultsUpdated -= DisplayResult;
        }

        private void DisplayResult()
        {
            panel.SetActive(true);

            resultText.text = StringTime.SecondToTimeString(raceResultTime.CurrentTime);
            goldTimeText.text = StringTime.SecondToTimeString(raceResultTime.GoldTime);
            recordText.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);

            congratulations.SetActive(raceResultTime.CurrentTime == raceResultTime.PlayerRecordTime);
        }
    }
}

