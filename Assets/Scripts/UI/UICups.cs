using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    [RequireComponent(typeof (UIRaceButton))]
    public class UICups : MonoBehaviour
    {
        [SerializeField] private Image goldCup;
        [SerializeField] private Image silverCup;
        [SerializeField] private Image bronzeCup;
        
        private UIRaceButton uiRaceButton;

        private void Start()
        {
            uiRaceButton = GetComponent<UIRaceButton>();
            CheckCups();
        }

        private void CheckCups()
        {
            TurnOffAllCups();
            var result = LevelUtil.FindSavedPlayerRecordTimeByLevel(uiRaceButton.RaceInfo.SceneName);

            if (result == 0) return;

            if (result <= uiRaceButton.RaceInfo.BronzeTime)
                bronzeCup.enabled = true;

            if (result <= uiRaceButton.RaceInfo.SilverTime)
                silverCup.enabled = true;

            if (result <= uiRaceButton.RaceInfo.GoldTime)
                goldCup.enabled = true;
        }

        private void TurnOffAllCups()
        {
            bronzeCup.enabled = false;
            silverCup.enabled = false;
            goldCup.enabled = false;
        }
    }
}
