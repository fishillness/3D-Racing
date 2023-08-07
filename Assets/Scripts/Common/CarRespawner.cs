using UnityEngine;

namespace Racing
{
    public class CarRespawner : MonoBehaviour,
        IDependency<RaceStateTracker>, IDependency<Car>, IDependency<CarInputControl>
    {
        [SerializeField] private float respawnHeight;
        private TrackPoint respawnTrackPoint;

        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        private Car car;
        public void Construct(Car obj) => car = obj;

        private CarInputControl carInputControl;
        public void Construct(CarInputControl obj) => carInputControl = obj;

        private void Start()
        {
            raceStateTracker.OnTrackPointPassed += OnTrackPointPassed;
        }

        private void Update()
        {
            //DEBUG
            if (Input.GetKeyDown(KeyCode.R) == true)
            {
                Respawn();
            }
        }

        private void OnDestroy()
        {
            raceStateTracker.OnTrackPointPassed -= OnTrackPointPassed;
        }

        private void OnTrackPointPassed(TrackPoint trackPoint)
        {
            respawnTrackPoint = trackPoint;
        }

        public void Respawn()
        {
            if (respawnTrackPoint == null) return;
            if (raceStateTracker.RaceState != RaceState.Race) return;

            car.Respawn(respawnTrackPoint.transform.position
                + respawnTrackPoint.transform.up * respawnHeight,
                respawnTrackPoint.transform.rotation);

            carInputControl.Reset();
        }
    }
}
