using UnityEngine;

namespace Racing
{
    public class WindSound : MonoBehaviour
    {
        [SerializeField] private Car car;
        [SerializeField] private AudioSource windAudioSource;
        [SerializeField]
        [Range(0.0f, 1.0f)] private float normalizeSpeed;

        private void Update()
        {
            if (car.NormaliaLinearVelocity >= normalizeSpeed)
            {
                if (windAudioSource.isPlaying == false)
                    windAudioSource.Play();
            }
            else
            {
                windAudioSource.Stop();
            }
        }
    }
}

