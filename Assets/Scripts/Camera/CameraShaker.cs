using UnityEngine;

namespace Racing
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private Car car;
        [SerializeField]
        [Range(0.0f, 1.0f)] private float normalizeSpeedShake;
        [SerializeField] private float shakeAmount;

        private new Camera camera;

        private void Start()
        {
            camera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (car.NormaliaLinearVelocity >= normalizeSpeedShake)
                camera.transform.localPosition += Random.insideUnitSphere 
                    * shakeAmount * Time.deltaTime;
        }
    }
}

