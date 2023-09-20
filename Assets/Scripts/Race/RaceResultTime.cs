using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class RaceResultTime : MonoBehaviour,
        IDependency<RaceTimeTracker>, IDependency<RaceStateTracker>
    {
        public event UnityAction OnResultsUpdated;

        [SerializeField] private RaceInfo raceInfo;

        private float goldTime;
        private float silverTime;
        private float bronzeTime;
        private float playerRecordTime;
        private float currentTime;

        public float GoldTime => goldTime;
        public float SilverTime => silverTime;
        public float BronzeTime => bronzeTime;
        public float PlayerRecordTime => playerRecordTime;
        public float CurrentTime => currentTime;
        public bool RecordWasSet => playerRecordTime != 0;

        private RaceTimeTracker raceTimeTracker;
        public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;

        private RaceStateTracker raceStateTracker;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

        private void Awake()
        {
            ApplyProperties(raceInfo);
            Load();
        }

        private void Start()
        {
            raceStateTracker.OnCompleted += OnRaceCompleted;
        }

        private void OnDestroy()
        {
            raceStateTracker.OnCompleted -= OnRaceCompleted;
        }

        private void OnRaceCompleted()
        {
            float absoluteRecord = GetAbsoluteRecord();

            if (raceTimeTracker.CurrentTime < absoluteRecord || playerRecordTime == 0)
            {
                playerRecordTime = raceTimeTracker.CurrentTime;
                Save();
            }
            currentTime = raceTimeTracker.CurrentTime;
            OnResultsUpdated?.Invoke();
        }

        private void ApplyProperties(RaceInfo property)
        {
            goldTime = property.GoldTime;
            silverTime = property.SilverTime;
            bronzeTime = property.BronzeTime;
        }

        public float GetAbsoluteRecord()
        {
            if (playerRecordTime < goldTime && playerRecordTime != 0)
            {
                return playerRecordTime;
            }
            else
            {
                return goldTime;
            }
        }

        #region Temporarily 

        private void Load()
        {
            playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + 
                Constants.SaveMarkPlayerRecordTime, 0); 
        }

        private void Save()
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name +
                Constants.SaveMarkPlayerRecordTime, playerRecordTime);
        }

        #endregion
    }
}
