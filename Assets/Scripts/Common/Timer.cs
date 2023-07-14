using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public class Timer : MonoBehaviour
    {
        public event UnityAction OnFinished;

        [SerializeField] private float time;
        private float value;
        public float Value => value;

        private void Start()
        {
            value = time;
        }

        private void Update()
        {
            value -= Time.deltaTime;

            if (value <= 0)
            {
                enabled = false;
                OnFinished?.Invoke();
            }
        }
    }
}
