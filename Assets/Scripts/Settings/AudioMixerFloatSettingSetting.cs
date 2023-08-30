using UnityEngine;
using UnityEngine.Audio;

namespace Racing
{
    [CreateAssetMenu]
    public class AudioMixerFloatSettingSetting : Setting
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private string nameParameter;

        // -80..0
        [SerializeField] private float minRealValue;
        [SerializeField] private float maxRealValue;
        // 0..100
        [SerializeField] private float virtualStep;
        [SerializeField] private float minVirtualValue;
        [SerializeField] private float maxVirtualValue;

        private float currentValue = 0;
        
        public override bool IsMinValue { get => currentValue == minRealValue; }
        public override bool IsMaxValue { get => currentValue == maxRealValue; }

        public override void SetNextValue()
        {
            AddValue(Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
        }

        public override void SetPreviousValue()
        {
            AddValue(-Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
        }

        public override string GetStringValue()
        {
            return Mathf.Lerp(minVirtualValue, maxVirtualValue,
                (currentValue - minRealValue) / (maxRealValue - minRealValue)).ToString();
        }

        public override object GetValue()
        {
            return currentValue;
        }

        public override void Apply()
        {
            audioMixer.SetFloat(nameParameter, currentValue);
            Save();
        }

        private void AddValue(float value)
        {
            currentValue += value;
            currentValue = Mathf.Clamp(currentValue, minRealValue, maxRealValue);
        }

        public override void Load()
        {
            currentValue = PlayerPrefs.GetFloat(title, 0);
        }

        private void Save()
        {
            PlayerPrefs.SetFloat(title, currentValue);
        }
    }
}

