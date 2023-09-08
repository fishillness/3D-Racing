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
        [SerializeField] private Text silverTimeText;
        [SerializeField] private Text bronzeTimeTetx;
        [SerializeField] private Text recordText;
        [SerializeField] private GameObject nextButton;

        private SceneLoader sceneLoader;
        private RaceResultTime raceResultTime;
        public void Construct(RaceResultTime obj) => raceResultTime = obj;
        
        private void Start()
        {
            raceResultTime.OnResultsUpdated += DisplayResult;
            panel.SetActive(false);
            sceneLoader = gameObject.GetComponent<SceneLoader>();
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
            silverTimeText.text = StringTime.SecondToTimeString(raceResultTime.SilverTime);
            bronzeTimeTetx.text = StringTime.SecondToTimeString(raceResultTime.BronzeTime);
            recordText.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);

            congratulations.SetActive(raceResultTime.CurrentTime == raceResultTime.PlayerRecordTime);

            if (sceneLoader.IsLastLevel)
                nextButton.SetActive(false);
        }
    }
}

