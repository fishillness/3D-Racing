using UnityEngine;

namespace Racing
{
    public class SceneDependenciesContainer : Dependency
    {
        [Header("Car")]
        [SerializeField] private Car car;
        [SerializeField] private CarInputControl carInputControl;
        [SerializeField] private CarCameraController carCameraController;
        [Header("Others")]
        [SerializeField] private TrackPointCircuit trackPointCircuit;
        [SerializeField] private RaceStateTracker raceStateTracker;
        [SerializeField] private RaceTimeTracker raceTimeTracker;
        [SerializeField] private RaceResultTime raceResultTime;

        private void Awake()
        {
            FindAllObjectToBind();
        }

        protected override void BindAll(MonoBehaviour monoBehaviourInScene)
        {
            Bind<Car>(car, monoBehaviourInScene);
            Bind<CarInputControl>(carInputControl, monoBehaviourInScene);
            Bind<CarCameraController>(carCameraController, monoBehaviourInScene);
            Bind<TrackPointCircuit>(trackPointCircuit, monoBehaviourInScene);
            Bind<RaceStateTracker>(raceStateTracker, monoBehaviourInScene);
            Bind<RaceTimeTracker>(raceTimeTracker, monoBehaviourInScene);
            Bind<RaceResultTime>(raceResultTime, monoBehaviourInScene);
        }

    }
}
