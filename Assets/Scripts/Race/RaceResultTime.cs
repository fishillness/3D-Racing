using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public class RaceResultTime : MonoBehaviour,
        IDependency<RaceTimeTracker>, IDependency<RaceStateTracker>, IDependency<LevelDefiner>
    {
        public event UnityAction OnResultsUpdated;

        private RaceInfo raceInfo;
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

        private LevelDefiner levelDefiner;
        public void Construct(LevelDefiner obj) => levelDefiner = obj;

        private void Start()
        {
            raceInfo = levelDefiner.RaceInfo;
            ApplyProperties(raceInfo);
            playerRecordTime = Saves.LoadFloat(levelDefiner.SceneName +
                Constants.SaveMarkPlayerRecordTime, 0);

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
                Saves.SaveFloat(levelDefiner.SceneName +
                    Constants.SaveMarkPlayerRecordTime, playerRecordTime);
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
    }
}
