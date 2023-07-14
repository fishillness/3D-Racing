using UnityEngine;

namespace Racing
{
    public class CarCameraShaker : CarCameraComponent
    {
        [SerializeField]
        [Range(0.0f, 1.0f)] private float normalizeSpeedShake;
        [SerializeField] private float shakeAmount;

        private void Update()
        {
            if (car.NormaliaLinearVelocity >= normalizeSpeedShake)
                transform.localPosition += Random.insideUnitSphere 
                    * shakeAmount * Time.deltaTime;
        }
    }
}

