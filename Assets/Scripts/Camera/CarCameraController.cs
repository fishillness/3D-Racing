using UnityEngine;

namespace Racing
{
    public class CarCameraController : MonoBehaviour
    {
        [SerializeField] private Car car;
        [SerializeField] private new Camera camera;
        [SerializeField] private CarCameraFollow follower;
        [SerializeField] private CarCameraShaker shaker;
        [SerializeField] private CarCameraFovCorrector fovCorrector;
        //[SerializeField] private CarCameraFollow pathFollower;

        [SerializeField] private CarCameraVignetteController vignetteController;

        private void Awake()
        {
            follower.SetProperties(car, camera);
            shaker.SetProperties(car, camera);
            fovCorrector.SetProperties(car, camera);
            vignetteController.SetProperties(car, camera); //
        }
    }
}
