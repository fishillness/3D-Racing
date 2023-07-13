using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public class TrackPointCircuit : MonoBehaviour
    {
        #region Properties
        public event UnityAction<TrackPoint> OnTrackPointTriggered;
        public event UnityAction<int> OnCompletedLap;

        [SerializeField] private TrackType type;
        
        private TrackPoint[] points;
        private int lapsCompleted = -1;
        #endregion

        #region Unity Events

        private void Awake()
        {
            BuildCircuit();
        }

        private void Start()
        {
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
        #endregion

        #region Private methods
        [ContextMenu(nameof(BuildCircuit))]
        private void BuildCircuit()
        {
            points = new TrackPoint[transform.childCount];

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = transform.GetChild(i).GetComponent<TrackPoint>();

                if (points[i] == null)
                {
                    Debug.LogError("There is no TrackPoint script on one of the child objects.");
                    return;
                }

                points[i].Reset();
            }

            for (int i = 0; i < points.Length - 1; i++)
            {
                points[i].Next = points[i + 1];
            }

            points[0].IsFirst = true;

            if (type == TrackType.Circular)
            {
                points[0].IsLast = true;
                points[points.Length - 1].Next = points[0];
            }

            if (type == TrackType.Sprint)
                points[points.Length - 1].IsLast = true;
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
        #endregion
    }
}

