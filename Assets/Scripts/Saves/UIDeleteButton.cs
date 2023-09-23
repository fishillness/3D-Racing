using UnityEngine;

namespace Racing
{
    public class UIDeleteButton : MonoBehaviour
    {
        [SerializeField] private UIRaceButtonBlocker[] uiRaceButtonBlockers;
        
        public void DeleteAllSaves()
        {
            Saves.DeleteAllSaves();

            for (int i = 0; i < uiRaceButtonBlockers.Length; i++)
            {
                uiRaceButtonBlockers[i].BlockUIRaceButtons();
            }
        }
    }
}

