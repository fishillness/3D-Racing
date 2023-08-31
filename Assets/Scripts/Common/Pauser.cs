using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public class Pauser : MonoBehaviour
    {
        public event UnityAction<bool> PauseStateChange;

        private bool isPause;
        public bool IsPause => isPause;

        public void Pause()
        {
            if (isPause == true) return;

            Time.timeScale = 0;
            isPause = true;
            PauseStateChange?.Invoke(isPause);
        }

        public void UnPause()
        {
            if (isPause == false) return;

            Time.timeScale = 1;
            isPause = false;
            PauseStateChange?.Invoke(isPause);
        }

        public void ChangePauseState()
        {
            if (isPause == true)
                UnPause();
            else
                Pause();
        }

    }
}
