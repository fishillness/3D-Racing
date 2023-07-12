using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public class TrackPoint : MonoBehaviour
    {
        public event UnityAction<TrackPoint> OnTriggered;

        public TrackPoint Next;
        public bool IsFirst;
        public bool IsLast;

        protected bool isTarget;
        public bool IsTarget => isTarget;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.GetComponent<Car>() == null) return;

            OnTriggered?.Invoke(this);
        }

        public void Passed()
        {
            isTarget = false;
        }

        public void AssignAsTarget()
        {
            isTarget = true;
        }

    }
}

