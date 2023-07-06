using UnityEngine;

namespace Racing 
{
    [RequireComponent(typeof(CarChassis))]
    public class Car : MonoBehaviour
    {
        #region Properties
        [SerializeField] private float maxSteerAngle;
        [SerializeField] private float maxBrakeTorque;
        [SerializeField] private float maxSpeed;

        [Header("Engine")]
        [SerializeField] private AnimationCurve engineTorqueCurve;
        [SerializeField] private float engineMaxTorque;
        [SerializeField] private float engineMinRpm;
        [SerializeField] private float engineMaxRpm;
        [Header("DEBUG-Engine")]
        [SerializeField] private float engineTorque;
        [SerializeField] private float engineRpm;

        [Header("Gearbox")]
        [SerializeField] private float[] gears;
        [SerializeField] private float finalDriveRatio;
        [SerializeField] private float rearGear;
        [SerializeField] private float upShiftEngineRpm;
        [SerializeField] private float downShiftEngineRpm;
        [Header("DEBUG-Gearbox")]
        [SerializeField] private float selectedGear;
        [SerializeField] private int selectedGearIndex;

        private CarChassis chassis;

        [Header("DEBUG")]
        public float linearVelocity;
        public float ThrottleControl;
        public float SteerControl;
        public float BrakeControl;
        //public float HandBrakeControl;

        public float MaxSpeed => maxSpeed;
        public float LinearVelocity => chassis.LinearVelocity;
        public float NormaliaLinearVelocity => chassis.LinearVelocity / maxSpeed;
        public float WheelSpeed => chassis.GetWheelSpeed();
        public float SelectedGearIndex => selectedGearIndex;
        public float EngineRpm => engineRpm;
        public float EngineMaxRpm => engineMaxRpm;
        public float UpShiftEngineRpm => upShiftEngineRpm;
        #endregion

        #region Unity Events
        private void Start()
        {
            chassis = GetComponent<CarChassis>();
        }

        private void Update()
        {
            //DEBUG
            linearVelocity = LinearVelocity;

            UpdateEngineTorque(); 
            AutoGearShift();


            if (LinearVelocity >= maxSpeed)
                engineTorque = 0;

            chassis.MotorTorque = engineTorque * ThrottleControl;
            chassis.SteerAngle = maxSteerAngle * SteerControl;
            chassis.BrakeTorque = maxBrakeTorque * BrakeControl;
        }
        #endregion

        #region Public methods

        #region Gear
        public void UpGear()
        {
            ShiftGear(selectedGearIndex + 1);
        }

        public void DownGear()
        {
            ShiftGear(selectedGearIndex - 1);
        }

        public void ShiftToReverseGear()
        {
            selectedGear = rearGear;
        }

        public void ShiftToFirstGear()
        {
            ShiftGear(0);
        }

        public void ShiftToNetral()
        {
            selectedGear = 0;
        }
        #endregion

        #endregion

        #region Private methods

        private void AutoGearShift()
        {
            if (selectedGear < 0) return;

            if (engineRpm >= upShiftEngineRpm)
                UpGear();

            if (engineRpm < downShiftEngineRpm)
                DownGear();
        }

        private void ShiftGear(int gearIndex)
        {
            gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length - 1);
            selectedGear = gears[gearIndex];
            selectedGearIndex = gearIndex;
        }

        private void UpdateEngineTorque()
        {
            engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAverageRpm() * selectedGear * finalDriveRatio);
            engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm);

            engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm)
                * engineMaxTorque * finalDriveRatio * Mathf.Sign(selectedGear) * gears[0];
        }
        #endregion
    }
}

