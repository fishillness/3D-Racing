using UnityEngine;

namespace Racing
{
    public class SceneDependencies : MonoBehaviour
    {
        [Header("Car")]
        [SerializeField] private Car car;
        [SerializeField] private CarInputControl carInputControl;
        [SerializeField] private CarCameraController carCameraController;
        [Header("Others")]
        [SerializeField] private TrackPointCircuit trackPointCircuit;
        [SerializeField] private RaceStateTracker raceStateTracker;
        [SerializeField] private RaceTimeTracker raceTimeTracker;

        private void Awake()
        {
            MonoBehaviour[] allMonoInScene = FindObjectsOfType<MonoBehaviour>();
            
            for (int i = 0; i < allMonoInScene.Length; i++)
            {
                Bind(allMonoInScene[i]);
            }
        }

        private void Bind(MonoBehaviour mono)
        {
            (mono as IDependency<Car>)?.Construct(car);
            (mono as IDependency<CarInputControl>)?.Construct(carInputControl);
            (mono as IDependency<CarCameraController>)?.Construct(carCameraController);
            (mono as IDependency<TrackPointCircuit>)?.Construct(trackPointCircuit);
            (mono as IDependency<RaceStateTracker>)?.Construct(raceStateTracker);
            (mono as IDependency<RaceTimeTracker>)?.Construct(raceTimeTracker);
        }
    }
}
