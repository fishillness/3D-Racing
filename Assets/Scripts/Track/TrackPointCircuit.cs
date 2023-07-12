using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public class TrackPointCircuit : MonoBehaviour
    {
        public event UnityAction<TrackPoint> OnTrackPointTriggered;
        public event UnityAction<int> OnCompletedLap;

        [SerializeField] private TrackType type;
        [SerializeField] private TrackPoint[] points;

        private int lapsCompleted = -1;

        private void Start()
        {
            OnCompletedLap += (t) => Debug.Log("Lap completed");

            for (int i = 0; i < points.Length; i++)
            {
                points[i].OnTriggered += TrackPointTriggered;
            }

            points[0].AssignAsTarget();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].OnTriggered -= TrackPointTriggered;
            }
        }

        private void TrackPointTriggered(TrackPoint trackPoint)
        {
            if (trackPoint.IsTarget == false) return;

            trackPoint.Passed();
            trackPoint.Next?.AssignAsTarget();
            OnTrackPointTriggered?.Invoke(trackPoint);

            if (trackPoint.IsLast == true)
            {
                lapsCompleted++;

                if (type == TrackType.Sprint)
                    OnCompletedLap?.Invoke(lapsCompleted);

                if (type == TrackType.Circular && lapsCompleted > 0)
                        OnCompletedLap?.Invoke(lapsCompleted);
            }
        }
    }
}

