using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(Camera))]
    public class CameraFovCorrector : MonoBehaviour
    {
        [SerializeField] private Car car;
        [SerializeField] private float minFieldOfView;
        [SerializeField] private float maxFieldOfView;

        private new Camera camera;
        private float defaultFov;

        private void Start()
        {
            camera = GetComponent<Camera>();
            camera.fieldOfView = defaultFov;
        }

        private void Update()
        {
            camera.fieldOfView = Mathf.Lerp(minFieldOfView, maxFieldOfView,
                car.NormaliaLinearVelocity);
        }
    }
}

