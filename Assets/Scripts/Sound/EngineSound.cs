using System;
using UnityEngine;

namespace Racing
{
    public class EngineSound : MonoBehaviour
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource engineAudioSource;
        [SerializeField] private AudioSource gearboxAudioSource;

        [Header("Engine Sound Modifiers")]
        [SerializeField] private float pitchModifier;
        [SerializeField] private float volumeModifier;
        [SerializeField] private float rpmModifier;

        [SerializeField] private float basePitch = 0.75f;
        [SerializeField] private float baseVolume = 0.1f;

        private Car car;

        private void Start()
        {
            car = transform.root.GetComponent<Car>();
            car.OnGearboxChanged += TurnOnGearboxSound;
        }

        private void Update()
        {
            engineAudioSource.pitch = basePitch + pitchModifier * 
                ((car.EngineRpm / car.EngineMaxRpm) * rpmModifier);
            engineAudioSource.volume = baseVolume + volumeModifier *
                (car.EngineRpm / car.EngineMaxRpm);
        }

        private void OnDestroy()
        {
            car.OnGearboxChanged -= TurnOnGearboxSound;
        }

        private void TurnOnGearboxSound()
        {
            gearboxAudioSource.Play();
        }
    }
}

