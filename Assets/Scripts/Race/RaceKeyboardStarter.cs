using UnityEngine;

namespace Racing
{
    public class RaceKeyboardStarter : MonoBehaviour
    {
        [SerializeField] private RaceStateTracker raceStateTracker;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) == true)
            {
                raceStateTracker.LaunchPreparationStart();
            }
        }
    }
}
