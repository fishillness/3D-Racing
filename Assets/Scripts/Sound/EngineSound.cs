using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(AudioSource))]
    public class EngineSound : MonoBehaviour
    {
        [SerializeField] private float pitchModifier;
        [SerializeField] private float volumeModifier;
        [SerializeField] private float rpmModifier;

        [SerializeField] private float basePitch = 0.75f;
        [SerializeField] private float baseVolume = 0.1f;

        private Car car;
        private AudioSource audioSource;

        private void Start()
        {
            car = transform.root.GetComponent<Car>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            audioSource.pitch = basePitch + pitchModifier * 
                ((car.EngineRpm / car.EngineMaxRpm) * rpmModifier);
            audioSource.volume = baseVolume + volumeModifier *
                (car.EngineRpm / car.EngineMaxRpm);
        }
    }
}

