using UnityEngine;

namespace Racing
{
    public class UIRaceButtonBlocker : MonoBehaviour
    {
        private UIRaceButton[] uiRaceButtons;

        private void Start()
        {
            uiRaceButtons = GetComponentsInChildren<UIRaceButton>();
            BlockUIRaceButtons();
        }

        private void BlockUIRaceButtons()
        {
            if (uiRaceButtons == null) return;

            for (int i = 1; i < uiRaceButtons.Length; i++)
            {
                if (LevelUtil.FindSavedPlayerRecordTimeByLevel(uiRaceButtons[i - 1].RaceInfo.SceneName) == 0)
                {
                    uiRaceButtons[i].SetNonInteractable();
                }
            }
        }
    }
}
