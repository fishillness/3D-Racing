using System;
using UnityEngine;

namespace Racing
{
    public class CarCameraController : MonoBehaviour, IDependency<RaceStateTracker>
    {
        [SerializeField] private Car car;
        [SerializeField] private new Camera camera;

        [Header("Camera Components")]
        [SerializeField] private CarCameraFollow follower;
        [SerializeField] private CarCameraShaker shaker;
        [SerializeField] private CarCameraFovCorrector fovCorrector;
        [SerializeField] private CarCameraPathFollower pathFollower;
        [SerializeField] private CarCameraVignetteController vignetteController;

        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        private void Awake()
        {
            follower.SetProperties(car, camera);
            shaker.SetProperties(car, camera);
            fovCorrector.SetProperties(car, camera);
            vignetteController.SetProperties(car, camera); //
        }

        private void Start()
        {
            raceStateTracker.OnPreparationStarted += OnPreparationStarted;
            raceStateTracker.OnCompleted += OnCompleted;

            follower.enabled = false;
            pathFollower.enabled = true;
        }

        private void OnDestroy()
        {
            raceStateTracker.OnPreparationStarted -= OnPreparationStarted;
            raceStateTracker.OnCompleted -= OnCompleted;
        }

        private void OnPreparationStarted()
        {
            follower.enabled = true;
            pathFollower.enabled = false;
        }

        private void OnCompleted()
        {
            pathFollower.enabled = true;
            pathFollower.StartMoveToNearestPoint();
            pathFollower.SetLookTarget(car.transform);
            follower.enabled = false;
        }
    }
}
