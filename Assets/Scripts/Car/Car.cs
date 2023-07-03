using UnityEngine;

namespace Racing 
{
    [RequireComponent(typeof(CarChassis))]
    public class Car : MonoBehaviour
    {
        [SerializeField] private float maxSteerAngle;
        [SerializeField] private float maxBrakeTorque;
        [SerializeField] private float maxSpeed;

        [Header("Engine")]
        [SerializeField] private AnimationCurve engineTorqueCurve;
        [SerializeField] private float engineMaxTorque;
        //DEBUG
        [SerializeField] private float engineTorque;
        //DEBUG
        [SerializeField] private float engineRpm;
        [SerializeField] private float engineMinRpm;
        [SerializeField] private float engineMaxRpm;

        private CarChassis chassis;

        [Header("DEBUG")]
        public float linearVelocity;
        public float ThrottleControl;
        public float SteerControl;
        public float BrakeControl;
        //public float HandBrakeControl;

        public float MaxSpeed => maxSpeed;
        public float LinearVelocity => chassis.LinearVelocity;
        public float WheelSpeed => chassis.GetWheelSpeed();

        private void Start()
        {
            chassis = GetComponent<CarChassis>();
        }

        private void Update()
        {
            //DEBUG
            linearVelocity = LinearVelocity;

            UpdateEngineTorque();
            
            if (LinearVelocity >= maxSpeed)
                engineTorque = 0;

            chassis.MotorTorque = engineTorque * ThrottleControl;
            chassis.SteerAngle = maxSteerAngle * SteerControl;
            chassis.BrakeTorque = maxBrakeTorque * BrakeControl;
        }

        private void UpdateEngineTorque()
        {
            engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAverageRpm() * 3.7f);
            engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm);

            engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque;
        }
    }
}

